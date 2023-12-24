using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Conexao.Recuperar;
public interface IRecuperarTodasConexoesUseCase
{
    Task<IList<RespostaUsuarioConectadoJson>> Executar();
}
