using Microsoft.AspNetCore.Mvc;
using Pedido.Api.Contracts.Pedido;
using Pedido.Application.Commands.Pedidos.CriarPedido;
using Pedido.Application.Queries.Pedidos.ObterPedidoPorId;
using Pedido.Application.Queries.Pedidos.ObterTodosPedidos;

namespace Pedido.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly CriarPedidoHandler _criarPedidoHandler;
    private readonly ObterPedidoPorIdQueryHandler _obterPorIdHandler;
    private readonly ObterTodosPedidosQueryHandler _obterTodosHandler;

    public PedidosController(
        CriarPedidoHandler criarPedidoHandler,
        ObterPedidoPorIdQueryHandler obterPorIdHandler,
        ObterTodosPedidosQueryHandler obterTodosHandler)
    {
        _criarPedidoHandler = criarPedidoHandler;
        _obterPorIdHandler = obterPorIdHandler;
        _obterTodosHandler = obterTodosHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CriarPedidoCommand(
            request.Itens.Select(i => new CriarPedidoItemDto(i.ProdutoId, i.Quantidade)).ToList());

        var id = await _criarPedidoHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var pedido = await _obterPorIdHandler.HandleAsync(new ObterPedidoPorIdQuery(id), cancellationToken);

        if (pedido is null)
            return NotFound();

        return Ok(MapToResponse(pedido));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        var pedidos = await _obterTodosHandler.HandleAsync(cancellationToken);

        var response = pedidos.Select(MapToResponse);
        return Ok(response);
    }

    private static PedidoResponse MapToResponse(Domain.Entities.PedidoModel pedido)
    {
        return new PedidoResponse
        {
            Id = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            DataPedido = pedido.DataPedido,
            Status = pedido.Status,
            Itens = pedido.Itens.Select(item => new PedidoItemResponse
            {
                Id = item.Id,
                ProdutoId = item.ProdutoId,
                Nome = item.Nome,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario
            }).ToList()
        };
    }
}
