using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios.Receita;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

namespace LivroDeReceitas.Application.UseCases.Receita.Registrar;
public class RegistrarReceitaUseCase : IRegistrarReceitaUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private IUsuarioLogado _usuarioLogado;
    private IReceitaWriteOnlyRepositorio _repositorio;


    public RegistrarReceitaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, IUsuarioLogado usuarioLogado,
        IReceitaWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
    }

    public async Task<RespostaReceitaJson> Executar(RequisicaoReceitaJson requisicao)
    {
        Validar(requisicao);

        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = _mapper.Map<Domain.Entidades.Receita>(requisicao);
        receita.UsuarioId = usuarioLogado.Id;

        await _repositorio.Registrar(receita);

        await _unidadeDeTrabalho.Commit();

        return _mapper.Map<RespostaReceitaJson>(receita);

    }

    private void Validar(RequisicaoReceitaJson requisicao)
    {
        var validator = new RegistrarReceitasValidator();
        var resultado = validator.Validate(requisicao);

        if(!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }

    }

}
