using System.Threading.Tasks;
using ClientesEProdutos.Menu.Menu;
using Infra.db.ConexaoBanco;
class Program
{
    static async Task Main()
    {
        var conexao = DatabaseConnection.ObterConexaoAberta();
        var menu = new Menu(conexao);
        await menu.Executar();
    }
}