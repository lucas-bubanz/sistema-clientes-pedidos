using ClientesEProdutos.Menu.Operacoes;

namespace ClientesEProdutos.Menu.Menu
{
    public class Menu
    {
        private readonly OperacoesMenu _operacoesMenu;
        public Menu()
        {
            _operacoesMenu = new OperacoesMenu();
        }

        public void Executar()
        {
            _operacoesMenu.MoldaCliente("Lucas", "01879117037", "Ivoti");
        }
    }
}