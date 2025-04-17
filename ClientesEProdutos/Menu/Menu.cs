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
            // _operacoesMenu.MoldaCliente("Lucas", "01879117037", "Ivoti");
            // _operacoesMenu.MoldaCliente("Gabriel", "529.982.247-25", "Dois Irmãos");
            // _operacoesMenu.MoldaCliente("Pedro", "111.444.777-35", "Travessão");
            // _operacoesMenu.MoldaCliente("Pedro Augusto Johann", "04153962040", "Dois Irmãos");
            // _operacoesMenu.MoldaCliente("Braitwaite", "92843529085", "Alvorada");
            // _operacoesMenu.MoldaCliente("Alanpa", "036.684.480-60", "Joinville");
            _operacoesMenu.MoldaCliente("Mateus", "36640111047", "Porto Alegre");
        }
    }
}