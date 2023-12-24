namespace LivroDeReceitas.Domain.Repositorios.Codigo;
public interface ICodigoReadOnlyRepositorio
{
    Task<Domain.Entidades.Codigos> RecuperarEntidadeCodigo(string codigo);
}
