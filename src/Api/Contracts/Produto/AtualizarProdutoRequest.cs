namespace Api.Contracts.Produto;

public sealed record AtualizarProdutoRequest(
    string Nome,
    string Descricao,
    decimal Preco
);
