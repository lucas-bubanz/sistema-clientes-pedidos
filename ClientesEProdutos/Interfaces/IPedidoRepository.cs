using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IPedidoRepository
    {
        Task<PrePedido> CriarPrePedidoAsync(PrePedido prePedido);
        Task<Pedido> ConfirmarPrePedidoAsync(int prePedidoId);
        Task<List<Pedido>> ListarPedidosAsync();
        Task<Pedido> ConsultarPedidoAsync(int pedidoId);
    }
}