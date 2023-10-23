using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios;
using Moq;

namespace UtilitarioParaOsTests.Repositorios;

public class UsuarioReadOnlyRepositorioBuilder
{
    private static UsuarioReadOnlyRepositorioBuilder _instance;
    private readonly Mock<UsuarioReadOnlyRepositorio> _repositorio;

    private UsuarioReadOnlyRepositorioBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<UsuarioReadOnlyRepositorio>();
        }

    }
    public static UsuarioReadOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioReadOnlyRepositorioBuilder();
        return _instance;
    }

    public UsuarioReadOnlyRepositorioBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repositorio.Setup(i => i.ExisteUsuarioComEmail(email)).ReturnsAsync(true);
        
        return this;
    }

    public UsuarioReadOnlyRepositorioBuilder RecuperarPorEmailSenha(Usuario usuario)
    {
        _repositorio.Setup(i => i.RecuperarPorEmailSenha(usuario.Email, usuario.Senha)).ReturnsAsync(usuario);

        return this;
    }

    public UsuarioReadOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}
