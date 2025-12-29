 using Api.Contracts.Produto;
using Application.Commands.Produtos.Atualizar;
using Application.Commands.Produtos.AtualizarDados;
using Application.Commands.Produtos.AtualizarPreco;
using Application.Commands.Produtos.CriarProduto;
using Application.Queries.Produtos.ObterPorId;
using Application.Queries.Produtos.ObterTodos;
using Microsoft.AspNetCore.Mvc;

namespace Produto.Api.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutosController : ControllerBase
{
    private readonly CriarProdutoHandler _criarHandler;
    private readonly ObterProdutoPorIdQueryHandler _obterPorIdHandler;
    private readonly ObterTodosProdutosQueryHandler _obterTodosHandler;
    private readonly AtualizarProdutoHandler _atualizarHandler;
    private readonly AtualizarPrecoHandler _atualizarPrecoHandler;
    private readonly AtualizarDadosProdutoHandler _atualizarDadosHandler;

    public ProdutosController(
        CriarProdutoHandler criarHandler,
        ObterProdutoPorIdQueryHandler obterPorIdHandler,
        ObterTodosProdutosQueryHandler obterTodosHandler,
        AtualizarProdutoHandler atualizarHandler,
        AtualizarPrecoHandler atualizarPrecoHandler,
        AtualizarDadosProdutoHandler atualizarDadosHandler)
    {
        _criarHandler = criarHandler;
        _obterPorIdHandler = obterPorIdHandler;
        _obterTodosHandler = obterTodosHandler;
        _atualizarHandler = atualizarHandler;
        _atualizarPrecoHandler = atualizarPrecoHandler;
        _atualizarDadosHandler = atualizarDadosHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoRequest request)
    {
        var command = new CriarProdutoCommand(
            request.Nome,
            request.Descricao,
            request.Preco);

        var id = await _criarHandler.HandleAsync(command);

        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var produto = await _obterPorIdHandler.HandleAsync(
            new ObterProdutoPorIdQuery(id));

        if (produto is null)
            return NotFound();

        return Ok(new ProdutoResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco
        });
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var produtos = await _obterTodosHandler.HandleAsync();

        return Ok(produtos);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarProdutoRequest request)
    {
        var command = new AtualizarProdutoCommand(
            id,
            request.Nome,
            request.Descricao,
            request.Preco);

        await _atualizarHandler.HandleAsync(command);

        return NoContent();
    }

    [HttpPatch("{id:guid}/preco")]
    public async Task<IActionResult> AtualizarPreco(
        Guid id,
        [FromBody] AtualizarPrecoRequest request)
    {
        var command = new AtualizarPrecoCommand(id, request.Preco);

        await _atualizarPrecoHandler.HandleAsync(command);

        return NoContent();
    }

    [HttpPatch("{id:guid}/dados")]
    public async Task<IActionResult> AtualizarDados(
        Guid id,
        [FromBody] AtualizarDadosProdutoRequest request)
    {
        var command = new AtualizarDadosProdutoCommand(
            id,
            request.Nome,
            request.Descricao);

        await _atualizarDadosHandler.HandleAsync(command);

        return NoContent();
    }
}
