
using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Domain.Repositorios.Conexao;
using LivroDeReceitas.Domain.Repositorios.Receita;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

namespace LivroDeReceitas.Application.UseCases.Conexao.Remover;
public class RemoverConexaoUseCase : IRemoverConexaoUseCase
{
    private readonly IConexaoReadOnlyRepositorio _repositorioReadOnly;
    private readonly IConexaoWriteOnlyRepositorio _repositorioWriteOnly;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RemoverConexaoUseCase(IUsuarioLogado usuarioLogado,
        IConexaoReadOnlyRepositorio repositorioReadOnly,
        IConexaoWriteOnlyRepositorio repositorioWriteOnly,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioReadOnly = repositorioReadOnly;
        _repositorioWriteOnly = repositorioWriteOnly; 
        _usuarioLogado = usuarioLogado;
        _unidadeDeTrabalho = unidadeDeTrabalho; 
    }

    public async Task Executar(long idUsuarioConectadoParaRemover)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var usuarioConectados = await _repositorioReadOnly.RecuperarDoUsuario(usuarioLogado.Id);

        Validar(usuarioConectados, idUsuarioConectadoParaRemover);

        await  _repositorioWriteOnly.RemoverConexao(usuarioLogado.Id, idUsuarioConectadoParaRemover); 

        await _unidadeDeTrabalho.Commit();

    }

    public static void Validar(IList<Domain.Entidades.Usuario> usuariosConectados, long idUsuarioConectadoParaRemover)
    {        

        if (!usuariosConectados.Any(c => c.Id == idUsuarioConectadoParaRemover))
        {
            throw new ErrosDeValidacaoException(new List<string> { ""});
        }

    }

}
