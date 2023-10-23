namespace LivroDeReceitas.Domain.Repositorios;

public interface UsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Entidades.Usuario> Login(string email, string senha);
}
