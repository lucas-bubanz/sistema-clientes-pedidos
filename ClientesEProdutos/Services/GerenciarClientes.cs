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

        public void CadastrarNovoCliente(Clientes clientes)
        {
            _ListaDeClientes.Add(clientes);
            string inserirClientesNaTabela = @"
                INSERT INTO clientes (nome_cliente, cpf_cliente, endereco_cliente)
                VALUES (@nome_cliente, @cpf_cliente, @endereco_cliente)
                RETURNING codigo_cliente;
            ";
            try
            {
                using NpgsqlCommand insereClienteNoBanco = new NpgsqlCommand(inserirClientesNaTabela, conexaoComBanco);
                insereClienteNoBanco.Parameters.AddWithValue("nome_cliente", clientes.NomeCliente);
                insereClienteNoBanco.Parameters.AddWithValue("cpf_cliente", clientes.CpfCliente);
                insereClienteNoBanco.Parameters.AddWithValue("endereco_cliente", clientes.EnderecoCliente);

                // Agora usamos ExecuteScalar para obter o código_cliente gerado
                clientes.CodigoCliente = Convert.ToInt32(insereClienteNoBanco.ExecuteScalar());
                Console.WriteLine($"Cliente {clientes.NomeCliente} adicionado com sucesso com ID: {clientes.CodigoCliente}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir Cliente no Banco: {ex}");
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
    }
}