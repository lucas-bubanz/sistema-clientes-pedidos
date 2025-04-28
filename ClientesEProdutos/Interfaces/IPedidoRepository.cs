using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IPedidoRepository
    {
        Task<PrePedido> CriarPrePedidoAsync(PrePedido prePedido);
        Task<Pedido> ConfirmarPrePedidoAsync(int prePedidoId);
        Task<List<PedidoDto>> ListarPedidosAsync(int page, int pageSize);
        Task<Pedido> ConsultarPedidoAsync(int pedidoId);
        Task<int> ObterTotalPedidosAsync();
    }
}