using Application.Common.Exceptions;
using Application.Interfaces;

namespace Application.Commands.Produtos.AtualizarDados
{
    public sealed class AtualizarDadosProdutoHandler
    {
        private readonly IProdutoRepository _repository;

        public AtualizarDadosProdutoHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AtualizarDadosProdutoCommand command)
        {
            var produto = await _repository.ObterPorIdAsync(command.ProdutoId);

            if (produto is null)
                throw new NotFoundException("Produto não encontrado.");

            produto.AtualizarDados(command.Nome, command.Descricao);

            await _repository.AtualizarAsync(produto);
        }
    }

}
