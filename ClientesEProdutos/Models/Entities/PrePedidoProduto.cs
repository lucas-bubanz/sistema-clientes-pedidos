using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesEProdutos.Models.Entities
{
    public class PrePedidoProduto
    {
        [Key]
        [Column("id_pre_pedido_produto")]
        public int IdPrePedidoProduto { get; set; }

        // Chave estrangeira para PrePedido
        [Required]
        [Column("id_pre_pedido")]
        public int PrePedidoId { get; set; }
        public PrePedido PrePedido { get; set; }

        // Chave estrangeira para Produto
        [Required]
        [Column("codigo_produto")]
        public int CodigoProduto { get; set; }
        public Produtos Produto { get; set; }

        // Quantidade do produto no pré-pedido
        [Required]
        [Column("quantidade")]
        public int Quantidade { get; set; }
    }
}