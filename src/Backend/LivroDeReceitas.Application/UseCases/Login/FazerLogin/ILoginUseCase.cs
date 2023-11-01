using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Login.FazerLogin;

public interface ILoginUseCase
{
    Task<RespostaLoginJson> Executar(RequisicaoLoginJson request) ;
}
