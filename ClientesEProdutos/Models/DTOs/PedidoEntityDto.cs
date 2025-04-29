namespace ClientesEProdutos.Models.DTOs
{
    public class PedidoEntityDto
    {
        public int IdPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }

        // Informações do Cliente
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }

        // Produtos do Pedido
        public List<ProdutoDto> Produtos { get; set; }
    }

    public class ProdutoDto
    {
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int Quantidade { get; set; }
    }

    public class PrePedidoDto
    {
        public int IdPrePedido { get; set; }
        public DateTime DataPedido { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public List<ProdutoDto> Produtos { get; set; }
    }
}