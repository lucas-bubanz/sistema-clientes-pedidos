using ClientesEProdutos.Models;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteEntityDto>> ListarClientesAsync(int page, int pageSize);
        Task<Clientes> ListarClientePorIdAsync(int id);
        Task AdicionarCleinteAsync(Clientes cliente);
        Task AtualizarClienteAsync(Clientes cliente);
        Task RemoverClienteAsync(int id);
        Task<int> ObterTotalClientesAsync();
    }
}