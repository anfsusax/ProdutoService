using Microsoft.AspNetCore.Mvc;
using Pedido.Application.Commands.Pedidos.CriarPedido;

namespace Pedido.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly CriarPedidoHandler _criarPedidoHandler;

    public PedidosController(CriarPedidoHandler criarPedidoHandler)
    {
        _criarPedidoHandler = criarPedidoHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarPedidoCommand command)
    {
        var id = await _criarPedidoHandler.Handle(command);
        return CreatedAtAction(nameof(ObterPorId), new { id }, id);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        return Ok(id); // Placeholder
    }
}
