using ClientesEProdutos.Models;
using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IPedidoRepository
    {
        Task<PrePedido> CriarPrePedidoAsync(PrePedido prePedido);
        Task<Pedido> ConfirmarPrePedidoAsync(int prePedidoId);
        Task<PrePedido> CancelarPrePedidoAsync(int prePedidoId);
        Task<Pedido> CancelarPedido(int idPedido);
        Task<List<PedidoDto>> ListarPedidosAsync(int page, int pageSize);
        Task<List<PrePedidoDto>> ListarPrePedidosAsync(int page, int pageSize);
        Task<Pedido> ConsultarPedidoAsync(int pedidoId);
        Task<PrePedido> ConsultarPrePedidoAsync(int prePedidoId);
        Task<int> ObterTotalPedidosAsync();
        Task<int> ObterTotalPrePedidosAsync();
    }
}