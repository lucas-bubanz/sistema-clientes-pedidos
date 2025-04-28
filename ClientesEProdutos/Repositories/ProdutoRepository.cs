using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDBContext.Data;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesEProdutos.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produtos> GetPorIdAsync(int id)
        {
            return await _context.produtos.FirstOrDefaultAsync(c => c.Codigo_produto == id);
        }

        public async Task<IEnumerable<Produtos>> ListarProdutosAsync(int page, int pageSize)
        {
            return await _context.produtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ObterTotalProdutosAsync()
        {
            return await _context.produtos.CountAsync();
        }

        public async Task AdicionarProdutoAsync(Produtos produto)
        {
            await _context.produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProdutoAsync(Produtos produto)
        {
            _context.produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverProdutoAsync(int id)
        {
            var produto = await _context.produtos.FirstOrDefaultAsync(c => c.Codigo_produto == id);
            if (produto != null)
            {
                _context.produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}