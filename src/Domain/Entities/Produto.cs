using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public bool Ativo { get; private set; }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();


    protected Produto() { } // EF Core

    public Produto(string nome, string descricao, decimal preco)
    {
        Validar(nome, preco);

        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Ativo = true;
    }

    public void Atualizar(string nome, string descricao, decimal preco)
    {
        Validar(nome, preco);
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
 
        AddDomainEvent(new ProdutoAtualizadoEvent(Id, Nome, Descricao, Preco, DateTime.UtcNow));
    }

    public void Desativar()
    {
        Ativo = false;
        AddDomainEvent(new ProdutoDesativadoEvent(Id, DateTime.UtcNow));
    }


    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new DomainException("Preço deve ser maior que zero.");

        Preco = novoPreco;
    }

    public void AtualizarDados(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório.");

        Nome = nome;
        Descricao = descricao;
    }
     
    private static void Validar(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome do produto é obrigatório.");

        if (preco <= 0)
            throw new DomainException("Preço deve ser maior que zero.");
    }

    public void AddDomainEvent(object @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
