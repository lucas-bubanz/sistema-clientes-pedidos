using System.Threading.Tasks;
using ClientesEProdutos.Menu.Operacoes;
using ClientesEProdutos.Services.GerenciarClientesApplicacao;
using Npgsql;

namespace ClientesEProdutos.Menu.Menu
{
    public class Menu
    {
        private readonly OperacoesMenu _operacoesMenu;
        private readonly GerenciarClientes _gerenciarClientes;

        public Menu(NpgsqlConnection conexao)
        {
            _gerenciarClientes = new GerenciarClientes(conexao);
            _operacoesMenu = new OperacoesMenu(_gerenciarClientes);
        }

        public async Task Executar()
        {
            // await _operacoesMenu.ListarClientes();
            // await _operacoesMenu.RemoverClientes();
            await _operacoesMenu.AtualizarClientes();
        }
    }
}