using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesEProdutos.Models
{
    public class PrePedidoEntityDto
    {
        public int IdPrePedido { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public List<ProdutoPrePedidoDto> Produtos { get; set; }
    }
    public class ProdutoPrePedidoDto
    {
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int Quantidade { get; set; }
    }
}