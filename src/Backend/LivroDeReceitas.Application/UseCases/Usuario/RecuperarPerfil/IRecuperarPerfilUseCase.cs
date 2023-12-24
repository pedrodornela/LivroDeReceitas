using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
public interface IRecuperarPerfilUseCase
{
    Task<RespostaPerfilUsuarioJson> Executar();
}
