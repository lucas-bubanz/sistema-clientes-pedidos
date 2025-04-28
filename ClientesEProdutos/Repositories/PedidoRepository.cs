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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PrePedido> CriarPrePedidoAsync(PrePedido prePedido)
        {
            await _context.PrePedidos.AddAsync(prePedido);
            await _context.SaveChangesAsync();
            return prePedido;
        }

        public async Task<Pedido> ConfirmarPrePedidoAsync(int prePedidoId)
        {
            var prePedido = await _context.PrePedidos
                .Include(pp => pp.PrePedidoProdutos)
                .ThenInclude(ppp => ppp.Produto)
                .FirstOrDefaultAsync(pp => pp.IdPrePedido == prePedidoId);

            if (prePedido == null) return null;

            var pedido = new Pedido
            {
                CodigoCliente = prePedido.CodigoCliente,
                Cliente = prePedido.Cliente,
                DataPedido = DateTime.UtcNow,
                ValorTotal = prePedido.PrePedidoProdutos.Sum(ppp => ppp.Produto.ValorProduto * ppp.Quantidade),
                PedidoProdutos = prePedido.PrePedidoProdutos.Select(ppp => new PedidoProduto
                {
                    CodigoProduto = ppp.CodigoProduto,
                    Produto = ppp.Produto,
                    Quantidade = ppp.Quantidade
                }).ToList()
            };

            _context.Pedidos.Add(pedido);

            _context.PrePedidos.Remove(prePedido);

            _context.Remove(prePedido);

            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<List<Pedido>> ListarPedidosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .Include(p => p.Cliente)
                .ToListAsync();
        }

        public async Task<Pedido> ConsultarPedidoAsync(int pedidoId)
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.IdPedido == pedidoId);
        }
    }
}