using System.ComponentModel.DataAnnotations;
using ClientesEProdutos.Enums;

namespace ClientesEProdutos.Models.Entities
{
    public class Clientes
    {
        [Key]
        public int Codigo_cliente { get; set; }
        [Required]
        public string? Nome_cliente { get; set; }
        public ETipoCliente Tipo_cliente { get; set; }
        public string? Endereco_cliente { get; set; }
    }
}