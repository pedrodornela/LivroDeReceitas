using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Filtros;

public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioAutenticadoAttribute(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenController = tokenController;
        _repositorio = repositorio;
            
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        try
        {
            var token = TokenNaRequisicao(context);
            var emailUsuario = _tokenController.RecuperarEmail(token);

            var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

            if (usuario is null)
            {
                throw new LivroDeReceitasException(string.Empty);
            }
        }
        catch (SecurityTokenExpiredException)
        {

            TokenExpirado(context);
        }
        catch 
        {
            UsuarioSemPermissao(context);
        }
        

    }

    private static string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if(string.IsNullOrWhiteSpace(authorization))
        {
            throw new LivroDeReceitasException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }


    private static void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }

    private static void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));
    }

}
