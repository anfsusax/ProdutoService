namespace Domain.Events
{
    public record ProdutoAtualizadoEvent(Guid ProdutoId, string Nome, string Descricao, decimal Preco, DateTime AtualizadoEm);
}
