using ClientesEProdutos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDBContext.Data
{
    public class AppDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Produtos> produtos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}