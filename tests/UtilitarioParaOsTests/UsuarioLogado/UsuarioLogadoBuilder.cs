using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Domain.Repositorios;
using Moq;
using UtilitarioParaOsTests.Repositorios;

namespace UtilitarioParaOsTests.UsuarioLogado;

public class UsuarioLogadoBuilder
{
    private static UsuarioLogadoBuilder _instance;
    private readonly Mock<IUsuarioLogado> _repositorio;

    private UsuarioLogadoBuilder()
    {
        if (_repositorio is null)
        {
            _repositorio = new Mock<IUsuarioLogado>();
        }

    }
    public static UsuarioLogadoBuilder Instancia()
    {
        _instance = new UsuarioLogadoBuilder();
        return _instance;
    }

    public UsuarioLogadoBuilder RecuperarUsuario(LivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        _repositorio.Setup(c => c.RecuperarUsuario()).ReturnsAsync(usuario);

        return this;
    }
    public IUsuarioLogado Construir()
    {
        return _repositorio.Object;
    }
}
