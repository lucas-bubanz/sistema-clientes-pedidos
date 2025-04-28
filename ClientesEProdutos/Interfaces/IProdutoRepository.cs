using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produtos>> ListarProdutosAsync(int page, int pageSize);
        Task<Produtos> GetPorIdAsync(int id);
        Task AdicionarProdutoAsync(Produtos produto);
        Task AtualizarProdutoAsync(Produtos produto);
        Task RemoverProdutoAsync(int id);
        Task<int> ObterTotalProdutosAsync();
    }
}