using Application.Interfaces;
using Application.Produtos.CriarProduto;
using Microsoft.AspNetCore.Mvc;
 

namespace Produto.Api.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(
        [FromBody] CriarProdutoCommand command,
        [FromServices] CriarProdutoHandler handler)
    {
        var id = await handler.Handle(command);
        return Created($"/produtos/{id}", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos(
        [FromServices] IProdutoRepository repository)
    {
        var produtos = await repository.ObterTodosAsync();
        return Ok(produtos);
    }
}
