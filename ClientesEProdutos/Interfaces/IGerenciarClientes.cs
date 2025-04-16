namespace ClientesEProdutos.Interfaces.IGerenciarClientes
{
    public interface IGerenciarClientes
    {
        void CadastrarNovoCliente(Models.Clientes.Clientes clientes);
        void ListarClientes();
        void RemoverClientes();
        void AtualizarClietnes();
    }
}