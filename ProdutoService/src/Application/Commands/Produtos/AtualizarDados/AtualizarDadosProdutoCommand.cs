namespace Application.Commands.Produtos.AtualizarDados;

public sealed record AtualizarDadosProdutoCommand(
    Guid ProdutoId,
    string Nome,
    string Descricao
);
