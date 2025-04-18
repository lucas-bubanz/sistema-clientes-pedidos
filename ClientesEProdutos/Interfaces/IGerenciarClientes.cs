namespace ClientesEProdutos.Interfaces.IGerenciarClientes
{
    public interface IGerenciarClientes
    {
        Task CadastrarNovoCliente(Models.Clientes.Clientes clientes);
        Task ListarClientes();
        Task RemoverClientes(string CpfCliente);
        void AtualizarClientes();
        bool ValidaEFormataCPF(string CpfCliente);
        int CalcularDigito(int[] numeros, int PesoInicial);
        bool ValidaCpfDuplicadoNoBanco(string cpf_cliente);
    }
}