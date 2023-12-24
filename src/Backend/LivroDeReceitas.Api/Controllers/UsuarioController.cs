using LivroDeReceitas.Api.Filtros.UsuarioLogado;
using LivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using LivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
using LivroDeReceitas.Application.UseCases.Usuario.Registrar;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Controllers
{
    public class UsuarioController : LivroDeReceitasController
    { 

        [HttpPost]
        [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarUsuario(
            [FromServices] IRegistrarUsuarioUseCase useCase, 
            [FromBody] RequisicaoRegistrarUsuarioJson request)
        {
            var resultado = await useCase.Executar(request);

            return Created(string.Empty, resultado);
        }

        [HttpPut]
        [Route("alterar-senha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> AlterarSenha(
            [FromServices] IAlterarSenhaUseCase useCase,
            [FromBody] RequisicaoAlterarSenhaJson request)
        {
            await useCase.Executar(request);

            return NoContent();
        }


        [HttpGet]
        [ProducesResponseType(typeof(RespostaPerfilUsuarioJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
        public async Task<IActionResult> RecuperarPerfil(
            [FromServices] IRecuperarPerfilUseCase useCase)
        {
            var resultado = await useCase.Executar();

            return Ok(resultado);
        }


    }
}