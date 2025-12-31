namespace Domain.Events
{
    public record ProdutoReativadoEvent(Guid ProdutoId, DateTime ReativadoEm);
}
