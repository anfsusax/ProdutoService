using Application.Common.Handlers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.Atualizar;

public  class AtualizarProdutoHandler : BaseHandler<Produto>
{
    private readonly IProdutoRepository _repository;

    public AtualizarProdutoHandler(IProdutoRepository repository, ILogger<AtualizarProdutoHandler> logger, IEventPublisher eventPublisher)
        : base(logger, eventPublisher)  
    {
        _repository = repository;
    }

    public async Task HandleAsync(AtualizarProdutoCommand command)
    {
        var produto = await _repository.ObterPorIdAsync(command.Id);
        if (produto == null) throw new KeyNotFoundException("Produto não encontrado");

        await HandleDomainEventsAsync(produto, async () =>
        {
            produto.Atualizar(command.Nome, command.Descricao, command.Preco);
            await _repository.AtualizarAsync(produto);
        }, produto.DomainEvents);

        produto.ClearDomainEvents();
    }
}
