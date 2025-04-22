namespace ClientesEProdutos.Interfaces
{
    public interface IValidaCPF
    {
        bool ValidaEFormataCPF(string CpfCliente);
        int CalcularDigito(int[] numeros, int PesoInicial);
        bool ValidaCpfDuplicadoNoBanco(string cpf_cliente);
    }
}