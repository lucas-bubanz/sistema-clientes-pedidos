using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Clientes;

namespace ClientesEProdutos.Services
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

        public void CadastrarNovoCliente()
        {
            throw new NotImplementedException();
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