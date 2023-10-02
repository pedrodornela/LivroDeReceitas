namespace LivroDeReceitas.Domain.Repositorios;

public interface UsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
}
