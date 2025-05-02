using System.ComponentModel.DataAnnotations;

namespace ClientesEProdutos.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Role { get; set; } = "User";
    }
}