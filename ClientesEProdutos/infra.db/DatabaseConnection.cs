using Npgsql;
using Microsoft.Extensions.Configuration;

namespace infra.db.ConexaoBanco
{
    class DatabaseConnection
    {

        public static NpgsqlConnection ObterConexaoAberta()
        {
            try
            {
                IConfiguration configuracaoApp = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("infra.db/appsettings.Development.json", optional: false, reloadOnChange: true)
                    .Build();

                string? stringConexao = configuracaoApp.GetConnectionString("PostgresConnection");

                NpgsqlConnection conexao = new NpgsqlConnection(stringConexao);
                conexao.Open();
                Console.WriteLine("✅ Conexão com o PostgreSQL aberta com sucesso!");
                return conexao;
            }
            catch (Exception erro)
            {
                Console.WriteLine($"❌ Erro ao conectar ao banco de dados: {erro.Message}");
                throw;
            }
        }
    }
}
