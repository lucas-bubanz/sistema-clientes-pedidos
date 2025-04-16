using Infra.db.ConexaoBanco;
using Npgsql;
class Program
{
    static void Main()
    {
        using NpgsqlConnection conexaoComBanco = DatabaseConnection.ObterConexaoAberta();
        string consultaTabelaClientes = "SELECT * FROM teste_clientes";

        using NpgsqlCommand comando = new NpgsqlCommand(consultaTabelaClientes, conexaoComBanco);
        using NpgsqlDataReader dados = comando.ExecuteReader();

        while (dados.Read())
        {
            int idCliente = dados.GetInt32(0);
            string nomeCliente = dados.GetString(1);

            Console.WriteLine($"ID: {idCliente}, Nome: {nomeCliente}");
        }
    }
}