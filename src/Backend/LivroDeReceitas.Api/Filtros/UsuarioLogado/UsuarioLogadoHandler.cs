﻿using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Filtros.UsuarioLogado;

public class UsuarioLogadoHandler : AuthorizationHandler<UsuarioLogadoRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioLogadoHandler(IHttpContextAccessor httpContextAccessor,
        TokenController tokenController,
        IUsuarioReadOnlyRepositorio repositorio)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController; 
        _repositorio = repositorio;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UsuarioLogadoRequirement requirement)
    {
        try
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();

            var emailUsuario = _tokenController.RecuperarEmail(token);

            var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

            if (usuario is null)
            {
                context.Fail();
                return;
            }

                context.Succeed(requirement);
            
        }
        catch 
        {
            context.Fail();
        }
        

    }


}
