using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Dashboard;
public interface IDashboardUseCase
{
    Task<RespostaDashboardJson> Executar(RequisicaoDashboardJson requisicao);
}
