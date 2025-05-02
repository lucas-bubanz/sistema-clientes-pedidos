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
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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
            modelBuilder.Entity<Pedido>().ToTable("pedidos");
            modelBuilder.Entity<PrePedidoProduto>().ToTable("pre_pedido_produto");
            modelBuilder.Entity<PedidoProduto>().ToTable("pedido_produto");
            modelBuilder.Entity<Clientes>().ToTable("cliente");
            modelBuilder.Entity<Produtos>().ToTable("produto");

            // Configurar relação PrePedido -> Clientes
            modelBuilder.Entity<PrePedido>()
                .HasOne(pp => pp.Cliente)
                .WithMany()
                .HasForeignKey(pp => pp.CodigoCliente)
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

            // Configurar relação PrePedidoProduto -> PrePedido
            modelBuilder.Entity<PrePedidoProduto>()
                .HasOne(ppp => ppp.PrePedido)
                .WithMany(pp => pp.PrePedidoProdutos)
                .HasForeignKey(ppp => ppp.PrePedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relação PrePedidoProduto -> Produtos
            modelBuilder.Entity<PrePedidoProduto>()
                .HasOne(ppp => ppp.Produto)
                .WithMany()
                .HasForeignKey(ppp => ppp.CodigoProduto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relação Pedido -> Clientes
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.CodigoCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relação PedidoProduto -> Pedido
            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relação PedidoProduto -> Produtos
            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.CodigoProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}