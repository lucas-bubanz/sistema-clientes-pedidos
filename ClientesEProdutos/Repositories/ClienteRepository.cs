using ApplicationDBContext.Data;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesEProdutos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarCleinteAsync(Clientes cliente)
        {
            await _context.clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarClienteAsync(Clientes cliente)
        {
            _context.clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Clientes> ListarClientePorIdAsync(int id)
        {
            return await _context.clientes.FirstOrDefaultAsync(i => i.Codigo_cliente == id);
        }

        public async Task<IEnumerable<Clientes>> ListarClientesAsync(int page, int pageSize)
        {
            return await _context.clientes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task RemoverClienteAsync(int id)
        {
            var cliente = await _context.clientes.FirstOrDefaultAsync(i => i.Codigo_cliente == id);
            if (cliente != null) _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ObterTotalClientesAsync()
        {
            return await _context.clientes.CountAsync();
        }
    }
}