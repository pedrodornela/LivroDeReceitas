namespace LivroDeReceitas.Domain.Repositorios.Conexao;
public interface IConexaoReadOnlyRepositorio
{
    Task<bool> ExisteConexao(long idUsuarioA, long idUsuarioB);
    Task<IList<Domain.Entidades.Usuario>> RecuperarDoUsuario(long usuarioId);
}
