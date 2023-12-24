namespace LivroDeReceitas.Application.UseCases.Conexao.Remover;
public interface IRemoverConexaoUseCase
{
    Task Executar(long idUsuarioConectadoParaRemovers);
}
