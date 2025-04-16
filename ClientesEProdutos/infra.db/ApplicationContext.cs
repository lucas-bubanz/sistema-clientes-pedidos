using Npgsql;
using Microsoft.Extensions.Configuration;

class ApplicationContext
{
    public static void Connect()
    {
        try
        {
            // Configuração para carregar o arquivo appsettings.Development.json
            var configuracao = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("infra.db/appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            //Obtendo string de conexão
            var stringDeConfiguracao = configuracao.GetConnectionString("PostgresConnection");

            using var conexao = new NpgsqlConnection(stringDeConfiguracao);
            conexao.Open();
            Console.WriteLine("Conexão com o PostgreSQL bem-sucessida");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
        }
    }
}
