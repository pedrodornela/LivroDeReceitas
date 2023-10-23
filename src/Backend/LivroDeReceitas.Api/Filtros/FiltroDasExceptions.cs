﻿using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Net;

namespace LivroDeReceitas.Api.Filtros;

public class FiltroDasExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is LivroDeReceitasExceptions) 
        {
            TratarLivroDeReceitasException(context);
        }
        else
        {
            TratarLoginException(context);
        }

    }

    private void TratarLivroDeReceitasException(ExceptionContext context)
    {
        if(context.Exception is ErrosDeValidacaoException)
        {
            TratarErrosDeValidacaoException(context);
        }
        else if(context.Exception is LoginInvalidoException)
        {
            LancarErroDesconhecido(context);
        }

    }

    private void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var erroDeVValidacaoException = context.Exception as ErrosDeValidacaoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(erroDeVValidacaoException.MensagensDeErro));
    }

    private void TratarLoginException(ExceptionContext context)
    {
        var erroLogin = context.Exception as LoginInvalidoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new RespostaErroJson(erroLogin.Message));
    }


    private void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new RespostaErroJson(ResourceMensagensDeErro.ERRO_DESCONHECIDO));   
    }

    

}
