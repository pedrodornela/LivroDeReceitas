using LivroDeReceitas.Domain.Repositorios;
using Moq;

namespace UtilitarioParaOsTests.Repositorios;

public class UsuarioWriteOnlyRepositorioBuilder
{
    private static UsuarioWriteOnlyRepositorioBuilder _instance;
    private readonly Mock<UsuarioWriteOnlyRepositorio> _repositorio;

    private UsuarioWriteOnlyRepositorioBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<UsuarioWriteOnlyRepositorio>(); 
        }

    }
    public static UsuarioWriteOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioWriteOnlyRepositorioBuilder();
        return _instance;
    }

    public UsuarioWriteOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }

}
