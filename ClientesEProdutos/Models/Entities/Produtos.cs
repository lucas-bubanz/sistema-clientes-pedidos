using System.ComponentModel.DataAnnotations;
using ClientesEProdutos.Enums;

namespace ClientesEProdutos.Models.Entities
{
    public class Produtos
    {
        [Key]
        public int Codigo_produto { get; set; }
        [Required]
        public string? Nome_produto { get; set; }
        public ETipoProduto Tipo_produto { get; set; }
        [Required]
        public decimal ValorProduto { get; set; }
    }
}