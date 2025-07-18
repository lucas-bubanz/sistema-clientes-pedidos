using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesEProdutos.Models.Entities
{
    public class Pedido
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        // Chave estrangeira para Cliente
        [Required]
        [Column("codigo_cliente")]
        public int CodigoCliente { get; set; }
        public Clientes Cliente { get; set; }

        [Required]
        [Column("data_pedido")]
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("valor_total")]
        public decimal ValorTotal { get; set; }

        // Coleção de produtos no pedido
        public ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }
}