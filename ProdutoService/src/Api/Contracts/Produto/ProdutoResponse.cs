namespace Api.Contracts.Produto
{
    public sealed class ProdutoResponse
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = null!;
        public string Descricao { get; init; } = null!;
        public decimal Preco { get; init; }
        public bool Ativo { get; init; }
        public DateTime DesativadoEm { get; init; }
        public DateTime? ReativadoEm { get; init; }
    }

}
