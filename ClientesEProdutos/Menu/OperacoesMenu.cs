using ClientesEProdutos.Interfaces.IGerenciarClientes;
using ClientesEProdutos.Models.Clientes;
using ClientesEProdutos.Services.GerenciarClientes;

namespace ClientesEProdutos.Menu.Operacoes
{
    public class OperacoesMenu
    {
        private readonly IGerenciarClientes _gerenciadorDeClientes;
        public Clientes cliente = new();
        public OperacoesMenu()
        {
            _gerenciadorDeClientes = new GerenciarClientes();
        }

        public void MoldaCliente(string NomeCliente, string CpfCliente, string EnderecoCliente)
        {
            cliente = new Clientes
            {
                NomeCliente = NomeCliente,
                CpfCliente = CpfCliente,
                EnderecoCliente = EnderecoCliente
            };

            _gerenciadorDeClientes.CadastrarNovoCliente(cliente);
        }
    }
}