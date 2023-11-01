using LivroDeReceitas.Domain.Entidades;

namespace LivroDeReceitas.Domain.Repositorios;

public interface IUsuarioUpdateOnlyRepositorio
{
    void Update(Usuario usuario);
    Task<Usuario> RecuperarPorId(long id);

}
