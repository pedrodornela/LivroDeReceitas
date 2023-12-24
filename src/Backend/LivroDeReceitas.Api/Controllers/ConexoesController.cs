using LivroDeReceitas.Api.Binder;
using LivroDeReceitas.Api.Filtros.UsuarioLogado;
using LivroDeReceitas.Application.UseCases.Conexao.Recuperar;
using LivroDeReceitas.Application.UseCases.Conexao.Remover;
using LivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class ConexoesController : LivroDeReceitasController
{

    [HttpGet]
    [ProducesResponseType(typeof(IList<RespostaUsuarioConectadoJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarConexoes([FromServices] IRecuperarTodasConexoesUseCase useCase)
    {
        var resultado = await useCase.Executar();

        if(resultado.Any())
        {
            return Ok(resultado);
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:hashids}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoverConexao(
        [FromServices] IRemoverConexaoUseCase useCase,
        [FromRoute] [ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Executar(id);

        return NoContent();
    }

}
