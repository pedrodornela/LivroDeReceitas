using Microsoft.Extensions.Configuration;

namespace LivroDeReceitas.Domain.Extension;

public static class RepositorioExtension
{
    public static string GetConexaoDatabase(this IConfiguration configurationManager)
    {
        var conexao = configurationManager.GetConnectionString("Conexao");
        return conexao;
    }

    public static string GetNomeDatabase(this IConfiguration configurationManager)
    {
        var nomedatabasde = configurationManager.GetConnectionString("NomeDataBase");
        return nomedatabasde;
    }


    public static string GetConexaoCompleta(this IConfiguration configurationManager)
    {
        var nomeDataBase = configurationManager.GetNomeDatabase();
        var conexao = configurationManager.GetConexaoDatabase();

        return $"{conexao}DataBase={nomeDataBase}";

    }


}
