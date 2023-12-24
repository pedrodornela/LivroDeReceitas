namespace LivroDeReceitas.Domain.Repositorios.Conexao;
public interface IConexaoWriteOnlyRepositorio
{
    Task Registrar(Domain.Entidades.Conexao conexao);
    Task RemoverConexao(long usuarioId, long usuarioIdParaRemover);
}
