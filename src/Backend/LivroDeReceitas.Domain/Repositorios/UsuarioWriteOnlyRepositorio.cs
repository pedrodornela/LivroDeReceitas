using LivroDeReceitas.Domain.Entidades;

namespace LivroDeReceitas.Domain.Repositorios;

public interface UsuarioWriteOnlyRepositorio
{
    Task Adicionar(Usuario usuario);
}
