using LivroDeReceitas.Comunicacao.Request;

namespace LivroDeReceitas.Application.UseCases.Usuario.AlterarSenha; 

public interface IAlterarSenhaUseCase
{
    Task Executar(RequisicaoAlterarSenhaJson requisicao);
}
