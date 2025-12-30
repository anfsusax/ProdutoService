namespace Api.Contracts.Produto
{
    public sealed class AtualizarDadosProdutoRequest
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
    }

}
