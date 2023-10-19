using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson requisicao);  
}
