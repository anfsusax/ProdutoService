namespace Domain.Events
{
    public record ProdutoCriadoEvent(Guid ProdutoId, string Nome, decimal Preco, DateTime CriadoEm);
}
