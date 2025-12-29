using Domain.Exceptions;

namespace Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public bool Ativo { get; private set; }

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
    }

    public void Desativar()
    {
        Ativo = false;
    }

    private static void Validar(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome do produto é obrigatório.");

        if (preco <= 0)
            throw new DomainException("Preço deve ser maior que zero.");
    }
}
