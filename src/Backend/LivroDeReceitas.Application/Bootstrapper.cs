using LivroDeReceitas.Application.Servicos.Criptografia;
using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivroDeReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
   
        AdicionarChaveAdicionaSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);


        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
    }

    private static void AdicionarChaveAdicionaSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");
        services.AddScoped(option => new EncriptadorDeSenha(section.Value));
    }

    private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoVida = configuration.GetRequiredSection("Configuracoes:TempoVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:ChaveToken");

        services.AddScoped(option => new TokenController(int.Parse(sectionTempoVida.Value), sectionKey.Value));
    }

}
