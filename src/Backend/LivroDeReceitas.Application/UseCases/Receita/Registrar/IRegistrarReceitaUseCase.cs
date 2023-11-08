using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Receita.Registrar;
public interface IRegistrarReceitaUseCase
{
    Task<RespostaReceitaJson> Executar(RequisicaoRegistrarReceitaJson requisicao);
}
