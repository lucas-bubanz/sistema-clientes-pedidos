using ClientesEProdutos.Menu.Menu;
using Infra.db.ConexaoBanco;
using Npgsql;
class Program
{
    static void Main()
    {
        using NpgsqlConnection conexaoComBanco = DatabaseConnection.ObterConexaoAberta();
        var menu = new Menu();
        menu.Executar();
    }
}