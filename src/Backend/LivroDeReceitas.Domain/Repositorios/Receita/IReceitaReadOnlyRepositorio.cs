namespace LivroDeReceitas.Domain.Repositorios.Receita;
public interface IReceitaReadOnlyRepositorio
{
    Task<IList<Entidades.Receita>> RecuperarTodasDoUsuario(long usuarioId);
    Task<Entidades.Receita> RecuperarPorId(long receitaId);
    Task<int> QuantidadeDeReceitas(long usuarioId);
    Task<IList<Entidades.Receita>> RecuperarTodasDosUsuarios(List<long> usuarioIds);
}
