using FluentMigrator.Runner;
using LivroDeReceitas.Domain.Extension;
using LivroDeReceitas.Domain.Repositorios;
using LivroDeReceitas.Infrastructure.AcessoRepositorio;
using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);

        AddContexto(services, configurationManager);
        AddUnidadeDeTrabalho(services);
        AddRepositorios(services);
    }

    private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Configuracoes:BancoDeDadosMemory").Value, out bool bancoDeDadosMemory);

        if (!bancoDeDadosMemory) 
        {
            var versaoServidor = new MySqlServerVersion(new Version(8, 0, 34));

            var connextionString = configurationManager.GetConexaoCompleta();

            services.AddDbContext<LivroDeReceitasContext>(dbContextoOpcoes =>
            dbContextoOpcoes.UseMySql(connextionString, versaoServidor));
        }
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
            .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>()
            .AddScoped<IUsuarioUpdateOnlyRepositorio, UsuarioRepositorio>();
    }
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
       _ = bool.TryParse(configurationManager.GetSection("Configuracoes:BancoDeDadosMemory").Value, out bool bancoDeDadosMemory);

        if (!bancoDeDadosMemory)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c =>
                c.AddMySql5()
                .WithGlobalConnectionString(configurationManager.GetConexaoCompleta()).ScanIn(Assembly.Load("LivroDeReceitas.Infrastructure")).For.All());
        }

    }

}
