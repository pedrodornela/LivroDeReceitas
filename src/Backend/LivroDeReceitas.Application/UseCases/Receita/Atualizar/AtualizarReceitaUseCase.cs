﻿using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Domain.Repositorios.Receita;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
using LivroDeReceitas.Application.UseCases.Receita.Registrar;

namespace LivroDeReceitas.Application.UseCases.Receita.Atualizar;
public class AtualizarReceitaUseCase : IAtualizarReceitaUseCase
{
    private readonly IReceitaUpdateOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AtualizarReceitaUseCase(IReceitaUpdateOnlyRepositorio repositorio, IUsuarioLogado usuarioLogado, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }
    public async Task Executar(long id, RequisicaoReceitaJson requisicao)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = await _repositorio.RecuperarPorId(id);

        Validar(usuarioLogado, receita, requisicao);

        _mapper.Map(requisicao, receita);

        _repositorio.Update(receita);

        await _unidadeDeTrabalho.Commit();

    }

    public static void Validar(Domain.Entidades.Usuario usuarioLogado, Domain.Entidades.Receita receita, RequisicaoReceitaJson requisicao)
    {
        if (receita is null || receita.UsuarioId != usuarioLogado.Id)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA });
        }

        /*var validator = new AtualizarReceitaValidator();
        var resultado = validator.Validate(requisicao);

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);

        }*/

    }

}
