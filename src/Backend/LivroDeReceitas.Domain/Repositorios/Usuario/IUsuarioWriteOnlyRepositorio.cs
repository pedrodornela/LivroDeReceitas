using LivroDeReceitas.Domain.Entidades;

namespace LivroDeReceitas.Domain.Repositorios;

public interface IUsuarioWriteOnlyRepositorio
{
    Task Adicionar(Usuario usuario);
}
