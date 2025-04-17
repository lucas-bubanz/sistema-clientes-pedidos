using ClientesEProdutos.Interfaces.IGerenciarClientes;
using ClientesEProdutos.Models.Clientes;
using Npgsql;
using Infra.db.ConexaoBanco;

namespace ClientesEProdutos.Services.GerenciarClientes
{
    public class GerenciarClientes : IGerenciarClientes
    {
        private readonly List<Clientes> _ListaDeClientes;
        public NpgsqlConnection conexaoComBanco = DatabaseConnection.ObterConexaoAberta();

        public GerenciarClientes()
        {
            _ListaDeClientes = new List<Clientes>();
        }

        public void AtualizarClietnes()
        {
            throw new NotImplementedException();
        }

        public async Task CadastrarNovoCliente(Clientes clientes)
        {
            await CarregaClientesBD(); //Carrega os dados já existentes no banco para a lista
            _ListaDeClientes.Add(clientes);

            if (conexaoComBanco.State != System.Data.ConnectionState.Open)
            {
                await conexaoComBanco.OpenAsync();
            }

            if (!ValidaEFormataCPF(clientes.CpfCliente)) // Validação dos dígitos
            {
                Console.WriteLine("CPF inválido!");
                return;
            }
            var CpfFormatado = clientes.CpfCliente;
            var cpf_cliente = new string(clientes.CpfCliente.Where(char.IsDigit).ToArray());

            if (ValidaCpfDuplicadoNoBanco(cpf_cliente))
            {
                Console.WriteLine($"CPF [{clientes.CpfCliente} - {clientes.NomeCliente}] já cadastrado na base de dados");
                return;
            }

            string inserirClientesNaTabela = @"
                INSERT INTO clientes (nome_cliente, cpf_cliente, endereco_cliente)
                VALUES (@nome_cliente, @cpf_cliente, @endereco_cliente)
                RETURNING codigo_cliente;
            ";
            using (var sessao = await conexaoComBanco.BeginTransactionAsync())
            {
                try
                {
                    using NpgsqlCommand insereClienteNoBanco = new NpgsqlCommand(inserirClientesNaTabela, conexaoComBanco, sessao);
                    insereClienteNoBanco.Parameters.AddWithValue("nome_cliente", clientes.NomeCliente);
                    insereClienteNoBanco.Parameters.AddWithValue("cpf_cliente", clientes.CpfCliente);
                    insereClienteNoBanco.Parameters.AddWithValue("endereco_cliente", clientes.EnderecoCliente);

                    clientes.CodigoCliente = Convert.ToInt32(await insereClienteNoBanco.ExecuteScalarAsync());

                    Console.WriteLine($"Cliente {clientes.NomeCliente} adicionado com sucesso com ID: {clientes.CodigoCliente}");
                    clientes.CpfCliente = CpfFormatado;

                    await sessao.CommitAsync();
                }
                catch (Npgsql.PostgresException FkCpf) when (FkCpf.SqlState == "23505")
                {
                    Console.WriteLine($"Erro: CPF já está cadastrado {FkCpf.Message}!");
                    // Rollback automático pelo 'using'
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao inserir Cliente no Banco: {ex.Message}");
                    // Rollback automático pelo 'using'
                }
            }
        }

        public void ListarClientes()
        {
            throw new NotImplementedException();
        }

        public void RemoverClientes()
        {
            throw new NotImplementedException();
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

            return digitos[9] == digito1 && digitos[10] == digito2;
        }

        public int CalcularDigito(int[] numeros, int PesoInicial)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
                soma += numeros[i] * (PesoInicial - i);

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }

        public bool ValidaCpfDuplicadoNoBanco(string CpfCliente)
        {
            string consultaCpfnoBanco = @"
                SELECT *
                FROM clientes
                WHERE cpf_cliente = @CpfCliente
            ";

            using NpgsqlCommand consultaCPF = new NpgsqlCommand(consultaCpfnoBanco, conexaoComBanco);
            consultaCPF.Parameters.AddWithValue("CpfCliente", CpfCliente);
            using NpgsqlDataReader resultado = consultaCPF.ExecuteReader();

            return resultado.Read(); // true = duplicado, false = não encontrado
        }

        public async Task CarregaClientesBD()
        {

            if (conexaoComBanco.State != System.Data.ConnectionState.Open)
            {
                await conexaoComBanco.OpenAsync();
            }

            string consultarClientesBDeInputarNaLista = @"
            SELECT *
            FROM clientes
            ";
            using (var sessao = await conexaoComBanco.BeginTransactionAsync())
            {
                try
                {
                    using NpgsqlCommand consultaClienteBD = new NpgsqlCommand(consultarClientesBDeInputarNaLista, conexaoComBanco, sessao);
                    consultaClienteBD.Parameters.AddWithValue("nome_cliente", "NomeClienteBanco");
                    consultaClienteBD.Parameters.AddWithValue("cpf_cliente", "CpfClienteBanco");
                    consultaClienteBD.Parameters.AddWithValue("endereco_cliente", "EnderecoClienteBanco");
                    using NpgsqlDataReader resultado = await consultaClienteBD.ExecuteReaderAsync();

                    while (await resultado.ReadAsync())
                    {
                        var cliente = new Clientes
                        {
                            CodigoCliente = Convert.ToInt32(resultado["codigo_cliente"]),
                            NomeCliente = resultado["nome_cliente"].ToString(),
                            CpfCliente = resultado["cpf_cliente"].ToString(),
                            EnderecoCliente = resultado["endereco_cliente"].ToString(),
                        };
                        _ListaDeClientes.Add(cliente);
                    }
                    await sessao.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar clientes do banco: {ex.Message}");
                }
            }
        }
    }
}