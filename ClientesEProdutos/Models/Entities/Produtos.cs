using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClientesEProdutos.Models.Entities
{
    public class Produtos
    {
        [Key]
        [Column("codigo_produto")]
        public int Codigo_produto { get; set; }

        [Required]
        [Column("nome_produto")]
        public string Nome_produto { get; set; }

        [Required]
        [Column("valor_produto")]
        public decimal ValorProduto { get; set; }

        [Column("descricao_produto")]
        public string DescricaoProduto { get; set; }
    }
}