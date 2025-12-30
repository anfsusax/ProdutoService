namespace Application.Commands.Produtos.Atualizar;

public sealed record AtualizarProdutoCommand(
    Guid Id,
    string Nome,
    string Descricao,
    decimal Preco
);
