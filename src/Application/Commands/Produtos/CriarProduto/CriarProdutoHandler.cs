using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.Produtos.CriarProduto;

public class CriarProdutoHandler
{
    private readonly IProdutoRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public CriarProdutoHandler(IProdutoRepository repository, IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Guid> HandleAsync(CriarProdutoCommand command)
    {
        var produto = new Produto(
            command.Nome,
            command.Descricao,
            command.Preco
        );

        await _repository.AdicionarAsync(produto);

        foreach (var domainEvent in produto.DomainEvents)
        {
            await _eventPublisher.PublishAsync(domainEvent);
        }
         
        produto.ClearDomainEvents();
         
        return produto.Id;
    }
}
