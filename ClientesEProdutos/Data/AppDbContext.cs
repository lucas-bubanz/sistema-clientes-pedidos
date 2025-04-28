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
        public DbSet<PrePedido> PrePedidos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrePedido>().ToTable("pre_pedidos");

            // Configurar relação PrePedido -> Clientes
            modelBuilder.Entity<PrePedido>()
                .HasOne(pp => pp.Cliente)
                .WithMany()
                .HasForeignKey(pp => pp.CodigoCliente)
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

            // Configurar relação PrePedido -> Produtos
            modelBuilder.Entity<PrePedido>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.CodigoProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}