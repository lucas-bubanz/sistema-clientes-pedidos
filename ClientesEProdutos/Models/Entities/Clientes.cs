using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesEProdutos.Models.Entities
{
    public class Clientes
    {
        [Key]
        [Column("codigo_cliente")]
        public int Codigo_cliente { get; set; }


        [Required]
        [Column("nome_cliente")]
        public string Nome_cliente { get; set; }

        [Column("endereco_cliente")]
        public string Endereco_cliente { get; set; }
    }
}