using LivroDeReceitas.Api.Filtros;
using LivroDeReceitas.Application.UseCases.Dashboard;
using LivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class DashboardController : LivroDeReceitasController
{

    [HttpPut]
    [ProducesResponseType(typeof(RespostaDashboardJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarDashboard(
            [FromServices] IDashboardUseCase useCase,
            [FromBody] RequisicaoDashboardJson request)
    {
        var resultado = await useCase.Executar(request);

        if(resultado.Receitas.Any())
        {
            return Ok(resultado);
        }

        return NoContent();
    }

}
