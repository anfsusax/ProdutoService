using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Queries.Produtos.ObterPorId
{
    public sealed class ObterProdutoPorIdQueryHandler
    {
        private readonly IProdutoRepository _repository;

        public ObterProdutoPorIdQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Produto> HandleAsync(ObterProdutoPorIdQuery query)
        {
            var produto = await _repository.ObterPorIdAsync(query.ProdutoId);

            if (produto is null)
                throw new NotFoundException("Produto não encontrado.");

            return produto;
        }
    }

}
