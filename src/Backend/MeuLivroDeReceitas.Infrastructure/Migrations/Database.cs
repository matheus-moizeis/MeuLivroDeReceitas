using Dapper;
using Microsoft.Data.SqlClient;

namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class Database
{
    public static void CriarDataBase(string conexaoComBancoDeDados, string nomeDatabase)
    {
        using var minhaConexao = new SqlConnection(conexaoComBancoDeDados);

        var parametros = new DynamicParameters();
        parametros.Add("nome", nomeDatabase);

        var registros = minhaConexao.Query("SELECT * FROM master.sys.databases WHERE name = @nome", parametros);

        if (!registros.Any())
        {
            minhaConexao.Execute($"CREATE DATABASE {nomeDatabase}");
        }
    }
}
