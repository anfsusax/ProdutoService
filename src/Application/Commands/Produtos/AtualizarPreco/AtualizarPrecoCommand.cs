namespace Application.Commands.Produtos.AtualizarPreco
{
    public sealed record AtualizarPrecoCommand(
     Guid ProdutoId,
     decimal Preco
 );

}
