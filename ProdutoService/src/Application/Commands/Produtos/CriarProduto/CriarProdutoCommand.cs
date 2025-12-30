namespace Application.Commands.Produtos.CriarProduto;

public record CriarProdutoCommand(
    string Nome,
    string Descricao,
    decimal Preco
);
