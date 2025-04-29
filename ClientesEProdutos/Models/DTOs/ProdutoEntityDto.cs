namespace ClientesEProdutos.Models
{
    public class ProdutoEntityDto
    {
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int Quantidade { get; set; }
    }
}