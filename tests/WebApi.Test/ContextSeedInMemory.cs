using LivroDeReceitas.Infrastructure.AcessoRepositorio;
using UtilitarioParaOsTests.Entidades;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (LivroDeReceitas.Domain.Entidades.Usuario usuario, string senha) Seed(LivroDeReceitasContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Construir();

        context.Usuarios.Add(usuario);

        context.SaveChanges();

        return (usuario,  senha);
    }


}
