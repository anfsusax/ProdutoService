namespace Domain.Events
{
    public record ProdutoDesativadoEvent(Guid ProdutoId, DateTime DesativadoEm);
}
