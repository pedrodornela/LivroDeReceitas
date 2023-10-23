using LivroDeReceitas.Application.UseCases.Login.FazerLogin;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Controllers;

public class LoginController : LivroDeReceitasController
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequisicaoLoginJson requisicao)
    {
        var resposta = await useCase.Executar(requisicao);
        return Ok(resposta);
    }


}
