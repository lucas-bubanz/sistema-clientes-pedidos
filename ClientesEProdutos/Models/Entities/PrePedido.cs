using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesEProdutos.Models.Entities
{
    public class PrePedido
    {
        [Key]
        [Column("id_pre_pedido")]
        public int IdPrePedido { get; set; }

        // Informações do Cliente        
        [Required]
        [Column("codigo_cliente")]
        public int CodigoCliente { get; set; }
        public Clientes Cliente { get; set; } // Chave estrangeira para Cliente

        [Required]
        [Column("nome_cliente")]
        public string NomeCliente { get; set; }

        [Required]
        [Column("endereco_cliente")]
        public string EnderecoCliente { get; set; }

        // Coleção de produtos no pré-pedido
        public ICollection<PrePedidoProduto> PrePedidoProdutos { get; set; }
    }
}