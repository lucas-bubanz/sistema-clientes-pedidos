using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesEProdutos.Models.Entities
{
    public class PedidoProduto
    {
        [Key]
        [Column("id_pedido_produto")]
        public int IdPedidoProduto { get; set; }

        // Chave estrangeira para Pedido
        [Required]
        [Column("id_pedido")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // Chave estrangeira para Produto
        [Required]
        [Column("codigo_produto")]
        public int CodigoProduto { get; set; }
        public Produtos Produto { get; set; }

        // Quantidade do produto no pedido
        [Required]
        [Column("quantidade")]
        public int Quantidade { get; set; }
    }
}