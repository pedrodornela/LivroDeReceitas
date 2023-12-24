using HashidsNet;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios.Codigo;
using LivroDeReceitas.Domain.Repositorios.Conexao;
using LivroDeReceitas.Exceptions.ExceptionsBase;

namespace LivroDeReceitas.Application.UseCases.Conexao.QRCodeLido;
public class QRCodeLidoUseCase : IQRCodeLidoUseCase
{
    private readonly IHashids _hashids;
    private readonly IConexaoReadOnlyRepositorio _repositorioConexao;
    private readonly ICodigoReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usarioLogado;

    public QRCodeLidoUseCase(ICodigoReadOnlyRepositorio repositorio,
        IUsuarioLogado usarioLogado,
        IConexaoReadOnlyRepositorio repositorioConexao,
        IHashids hashids)
    {
        _repositorio = repositorio;
        _usarioLogado = usarioLogado;
        _repositorioConexao = repositorioConexao;
        _hashids = hashids;
    }
    public async Task<(RespostaUsuarioConexaoJson usuarioParaSeConectar, string idUsuarioQueGerouQRCode)> Executar(string codigoConexao)
    {
        var usuarioLogado = await _usarioLogado.RecuperarUsuario();
        var codigo = await _repositorio.RecuperarEntidadeCodigo(codigoConexao);

        await Validar(codigo, usuarioLogado);

        var usuarioParaSeConectar = new RespostaUsuarioConexaoJson
        {
            Id = _hashids.EncodeLong(usuarioLogado.Id),
            Nome = usuarioLogado.Nome
        };

        return (usuarioParaSeConectar, _hashids.EncodeLong(codigo.UsuarioId));
    }

    private async Task Validar(Domain.Entidades.Codigos codigo, Domain.Entidades.Usuario usuarioLogado)
    {
        if (codigo is null)
        {
            throw new LivroDeReceitasException("");
        }

        if (codigo.UsuarioId == usuarioLogado.Id)
        {
            throw new LivroDeReceitasException("");
        }

         var existeConexao = await _repositorioConexao.ExisteConexao(codigo.UsuarioId, usuarioLogado.Id);

        if (existeConexao)
        {
            throw new LivroDeReceitasException("");
        }

    }
}
