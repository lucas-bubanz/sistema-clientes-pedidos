using ClientesEProdutos.Models;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoEntityDto>> ListarProdutosAsync(int page, int pageSize);
        Task<Produtos> GetPorIdAsync(int id);
        Task AdicionarProdutoAsync(Produtos produto);
        Task AtualizarProdutoAsync(Produtos produto);
        Task RemoverProdutoAsync(int id);
        Task<int> ObterTotalProdutosAsync();
    }
}