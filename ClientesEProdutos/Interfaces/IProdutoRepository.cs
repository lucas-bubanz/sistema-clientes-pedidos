using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produtos> GetProdutos();
        Produtos? GetPorId(int id);
        Task AdicionarProdutoAsync(Produtos produto);
        Task AtualizarProdutoAsync(Produtos produto);
        Task RemoverProdutoAsync(int id);
    }
}