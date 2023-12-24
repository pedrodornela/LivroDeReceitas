using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios;
using Microsoft.AspNetCore.Http;

namespace LivroDeReceitas.Application.Servicos.UsuarioLogado;

public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAcessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;
    public UsuarioLogado(IHttpContextAccessor httpContextAcessor, TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _httpContextAcessor = httpContextAcessor;
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task<Usuario> RecuperarUsuario()
    {
        var authorization = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var emailUsuario = _tokenController.RecuperarEmail(token);

        var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

        return usuario;
    }

}
