using AutoMapper;
using LivroDeReceitas.Application.Servicos.Criptografia;
using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

namespace LivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly IUsuarioWriteOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
        EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
    }

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio) 
    {
        _repositorio = repositorio;
    }

    public async Task<RespostaUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson requisicao)
    {
        await Validar(requisicao);

        var entidade = _mapper.Map<Domain.Entidades.Usuario>(requisicao);
        entidade.Senha = _encriptadorDeSenha.Criptografar(requisicao.Senha);
          
        await _repositorio.Adicionar(entidade);

        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(entidade.Email);

        return new RespostaUsuarioRegistradoJson
        {
            Token = token,
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJson requisicao)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(requisicao);

        var existeUsuarioComEmail = await _usuarioReadOnlyRepositorio.ExisteUsuarioComEmail(requisicao.Email);
        if(existeUsuarioComEmail)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));
        }


        if(!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
        
    }

}
