namespace ClientesEProdutos.Interfaces.IGerenciarClientes
{
    public interface IGerenciarClientes
    {
        Task CadastrarNovoCliente(Models.Clientes.Clientes clientes);
        void ListarClientes();
        void RemoverClientes();
        void AtualizarClietnes();
        bool ValidaEFormataCPF(string CpfCliente);
        int CalcularDigito(int[] numeros, int PesoInicial);
        bool ValidaCpfDuplicadoNoBanco(string cpf_cliente);
    }
}