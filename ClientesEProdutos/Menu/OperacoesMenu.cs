using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Interfaces.IGerenciarClientes;
using ClientesEProdutos.Models.Clientes;
using ClientesEProdutos.Services.GerenciarClientesApplicacao;
using Npgsql;

namespace ClientesEProdutos.Menu.Operacoes
{
    public class OperacoesMenu : IValidaCPF
    {
        private readonly IGerenciarClientes _gerenciadorDeClientes;
        public Clientes cliente = new();
        public OperacoesMenu(GerenciarClientes gerenciarClientes)
        {
            _gerenciadorDeClientes = gerenciarClientes;
        }
        public async Task ListarClientes()
        {
            await _gerenciadorDeClientes.ListarClientes();
        }

        public async Task RemoverClientes()
        {
            await _gerenciadorDeClientes.RemoverClientes("04153962040");
        }

        public async Task AtualizarClientes()
        {
            while (true)
            {
                Console.Write("O que você deseja atualiza? 1 - [Nome] | 2 - [Endereço] | 3 - [Sair]: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int opcao) || String.IsNullOrEmpty(entrada))
                {
                    Console.Write("Digite o CPF do cliente que deseja atualizar: ");
                    string cpfParaAtualizar = Console.ReadLine();
                    if (ValidaEFormataCPF(cpfParaAtualizar))
                    {
                        switch (opcao)
                        {
                            case 1:
                                Console.Write("Digite o novo nome do Cliente:");
                                string novoNomeDoCliente = Console.ReadLine();

                                string comandoDeAtualizacaoNome = $"UPDATE clientes SET nome_cliente = '{novoNomeDoCliente}' WHERE cpf_cliente = '{cliente.CpfCliente}'";
                                await _gerenciadorDeClientes.AtualizarClientes(comandoDeAtualizacaoNome, cpfParaAtualizar);
                                break;
                            case 2:
                                Console.Write("Digite o novo endereço do Cliente: ");
                                string novoEnderecoDoCliente = Console.ReadLine();

                                string comandoDeAtualizacaoEndereco = $"UPDATE clientes SET endereco_cliente = '{novoEnderecoDoCliente}' WHERE cpf_cliente = '{cliente.CpfCliente}'";
                                await _gerenciadorDeClientes.AtualizarClientes(comandoDeAtualizacaoEndereco, cpfParaAtualizar);
                                break;
                            case 3:
                                return;
                            default:
                                return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
                    continue;
                }
            }

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

        public bool ValidaEFormataCPF(string CpfCliente)
        {
            // Remove formatação
            CpfCliente = new string(CpfCliente.Where(char.IsDigit).ToArray());

            // Verifica se tem 11 dígitos e não é uma sequência repetida
            if (CpfCliente.Length != 11 || CpfCliente.Distinct().Count() == 1)
                return false;

            // Calcula os dígitos verificadores
            int[] digitos = CpfCliente.Select(c => int.Parse(c.ToString())).ToArray();
            int digito1 = CalcularDigito(digitos.Take(9).ToArray(), 10);
            int digito2 = CalcularDigito(digitos.Take(10).ToArray(), 11);

            if (digitos[9] == digito1 && digitos[10] == digito2)
            {
                return true; //Verdadeiro = Sem formatação
            }

            return false; //Falso = Com formatação
        }

        public int CalcularDigito(int[] numeros, int PesoInicial)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
                soma += numeros[i] * (PesoInicial - i);

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }

        public bool ValidaCpfDuplicadoNoBanco(string cpf_cliente)
        {
            throw new NotImplementedException();
        }
    }
}