using ApplicationDBContext.Data;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.DTOs;
using ProdutoDtoPedido = ClientesEProdutos.Models.DTOs.ProdutoDto;
using ClientesEProdutos.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

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

        public async Task<List<PedidoDto>> ListarPedidosAsync(int page, int pageSize)
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .Include(p => p.Cliente)
                .Select(p => new PedidoDto
                {
                    IdPedido = p.IdPedido,
                    DataPedido = p.DataPedido,
                    ValorTotal = p.ValorTotal,
                    NomeCliente = p.Cliente.Nome_cliente,
                    Produtos = p.PedidoProdutos.Select(pp => new ProdutoDtoPedido
                    {
                        CodigoProduto = pp.CodigoProduto,
                        NomeProduto = pp.Produto.Nome_produto,
                        ValorProduto = pp.Produto.ValorProduto,
                        Quantidade = pp.Quantidade
                    }).ToList()
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<PrePedidoDto>> ListarPrePedidosAsync(int page, int pageSize)
        {
            return await _context.PrePedidos
                .Include(pp => pp.PrePedidoProdutos)
                .ThenInclude(ppp => ppp.Produto)
                .Include(pp => pp.Cliente)
                .Select(pp => new PrePedidoDto
                {
                    IdPrePedido = pp.IdPrePedido,
                    // DataPedido = pp.DataPedido,
                    CodigoCliente = pp.CodigoCliente,
                    NomeCliente = pp.Cliente.Nome_cliente,
                    EnderecoCliente = pp.Cliente.Endereco_cliente,
                    Produtos = pp.PrePedidoProdutos.Select(ppp => new ProdutoDtoPedido
                    {
                        CodigoProduto = ppp.CodigoProduto,
                        NomeProduto = ppp.Produto.Nome_produto,
                        ValorProduto = ppp.Produto.ValorProduto,
                        Quantidade = ppp.Quantidade
                    }).ToList()
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<PrePedido> CancelarPrePedidoAsync(int prePedidoId)
        {
            var prePedido = await _context.PrePedidos.FirstOrDefaultAsync(p => p.IdPrePedido == prePedidoId);

            if (prePedido == null) { }

            _context.Remove(prePedido);
            await _context.SaveChangesAsync();

            return prePedido;
        }

        public async Task<Pedido> CancelarPedidoAsync(int idPedido)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.IdPedido == idPedido);

            if (pedido == null) { }

            _context.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<PrePedido> ConsultarPrePedidoAsync(int prePedidoId)
        {
            return await _context.PrePedidos.FirstOrDefaultAsync(p => p.IdPrePedido == prePedidoId);
        }

        public async Task<int> ObterTotalPedidosAsync()
        {
            return await _context.Pedidos.CountAsync();
        }

        public async Task<int> ObterTotalPrePedidosAsync()
        {
            return await _context.PrePedidos.CountAsync();
        }

    }
}