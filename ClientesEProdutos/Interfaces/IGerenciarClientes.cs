namespace ClientesEProdutos.Interfaces.IGerenciarClientes
{
    public interface IGerenciarClientes
    {
        Task CadastrarNovoCliente(Models.Clientes.Clientes clientes);
        Task ListarClientes();
        Task RemoverClientes(string CpfCliente);
        Task AtualizarClientes(string comandoDeAtualizacao, string cpfDoClienteParaAtualizar);
    }
}