using ClientesEProdutos.Interfaces.IGerenciarClientes;
using ClientesEProdutos.Models.Clientes;

namespace ClientesEProdutos.Services.GerenciarClientes
{
    public class GerenciarClientes : IGerenciarClientes
    {
        private readonly List<Clientes> _ListaDeClientes;

        public GerenciarClientes()
        {
            _ListaDeClientes = new List<Clientes>();
        }

        public void AtualizarClietnes()
        {
            throw new NotImplementedException();
        }

        public void CadastrarNovoCliente(Clientes clientes)
        {
            _ListaDeClientes.Add(clientes);
        }

        public void ListarClientes()
        {
            throw new NotImplementedException();
        }

        public void RemoverClientes()
        {
            throw new NotImplementedException();
        }
    }
}