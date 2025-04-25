using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Clientes>> GetClientes();
        Task<Clientes> GetClientePorId(int id);
        Task AdicionarCleinteAsync(Clientes cliente);
        Task AtualizarClienteAsync(Clientes cliente);
        Task RemoverClienteAsync(int id);
    }
}