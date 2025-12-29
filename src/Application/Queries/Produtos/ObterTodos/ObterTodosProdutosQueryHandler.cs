using Application.Interfaces;
using Domain.Entities;

namespace Application.Queries.Produtos.ObterTodos
{
    public sealed class ObterTodosProdutosQueryHandler
    {
        private readonly IProdutoRepository _repository;

        public ObterTodosProdutosQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> HandleAsync()
        {
            return await _repository.ObterTodosAsync();
        }
    }
}
