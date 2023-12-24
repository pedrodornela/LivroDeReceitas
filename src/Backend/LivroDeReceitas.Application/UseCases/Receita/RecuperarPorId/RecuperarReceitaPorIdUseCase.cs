using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios.Conexao;
using LivroDeReceitas.Domain.Repositorios.Receita;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;

namespace LivroDeReceitas.Application.UseCases.Receita.RecuperarPorId;
public class RecuperarReceitaPorIdUseCase : IRecuperarReceitaPorIdUseCase
{
    private readonly IConexaoReadOnlyRepositorio _conexoesRepositorio;
    private readonly IReceitaReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public RecuperarReceitaPorIdUseCase(IReceitaReadOnlyRepositorio repositorio,
        IUsuarioLogado usuarioLogado,
        IMapper mapper,
        IConexaoReadOnlyRepositorio conexoesRepositorio)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
        _conexoesRepositorio = conexoesRepositorio;
    }

    public async Task<RespostaReceitaJson> Executar(long id)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = await _repositorio.RecuperarPorId(id);

        await Validar(usuarioLogado, receita);

        return _mapper.Map<RespostaReceitaJson>(receita);
        
     }

    public async Task Validar(Domain.Entidades.Usuario usuarioLogado, Domain.Entidades.Receita receita)
    {
        var usuariosConectados = await _conexoesRepositorio.RecuperarDoUsuario(usuarioLogado.Id);

        if (receita is null || (receita.UsuarioId != usuarioLogado.Id && !usuariosConectados.Any(c => c.Id == receita.UsuarioId)))
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA});
        }

    }

}
