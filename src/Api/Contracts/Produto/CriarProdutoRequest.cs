namespace Api.Contracts.Produto
{
    public class CriarProdutoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }

}
