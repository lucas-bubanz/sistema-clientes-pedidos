using ClientesEProdutos.Interfaces.IGerenciarClientes;
using ClientesEProdutos.Models.Clientes;
using ClientesEProdutos.Services.GerenciarClientesApplicacao;

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

            if (String.IsNullOrEmpty(cliente.CpfCliente) || String.IsNullOrEmpty(cliente.NomeCliente))
            {
                Console.WriteLine("O CPF ou Nome do cliente não podem ser Nulo ou vázio.");
                return;
            }
            _gerenciadorDeClientes.CadastrarNovoCliente(cliente);
        }
    }
}