using Application.Common.Exceptions;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Produtos.AtualizarDados
{
    public sealed class AtualizarDadosProdutoHandler
    {
        private readonly IProdutoRepository _repository;
        private ILogger<AtualizarDadosProdutoHandler> _logger;
        public AtualizarDadosProdutoHandler(IProdutoRepository repository, ILogger<AtualizarDadosProdutoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task HandleAsync(AtualizarDadosProdutoCommand command)
        {
            _logger.LogInformation("Iniciando o processamento do comando AtualizarDadosProdutoCommand");

            var produto = await _repository.ObterPorIdAsync(command.ProdutoId);

            if (produto is null)
                throw new NotFoundException("Produto não encontrado.");

            produto.AtualizarDados(command.Nome, command.Descricao);

            await _repository.AtualizarAsync(produto);
            _logger.LogInformation("Comando AtualizarDadosProdutoCommand processado com sucesso");
        }
    }

}
