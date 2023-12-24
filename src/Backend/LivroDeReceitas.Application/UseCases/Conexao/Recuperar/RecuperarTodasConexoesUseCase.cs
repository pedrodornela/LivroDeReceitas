using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios.Conexao;
using LivroDeReceitas.Domain.Repositorios.Receita;

namespace LivroDeReceitas.Application.UseCases.Conexao.Recuperar;
public class RecuperarTodasConexoesUseCase : IRecuperarTodasConexoesUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IReceitaReadOnlyRepositorio _repositorioReceita;
    private readonly IConexaoReadOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;

    public RecuperarTodasConexoesUseCase(IUsuarioLogado usuarioLogado,
        IConexaoReadOnlyRepositorio repositorio,
        IMapper mapper, IReceitaReadOnlyRepositorio repositorioReceita)
    {
        _usuarioLogado = usuarioLogado;
        _repositorio = repositorio;
        _mapper = mapper;
        _repositorioReceita = repositorioReceita;
    }

    public async Task<IList<RespostaUsuarioConectadoJson>> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var conexoes = await _repositorio.RecuperarDoUsuario(usuarioLogado.Id);

        var tarefas = conexoes.Select(async usuario =>
        {
            var quantidadeReceitas = await _repositorioReceita.QuantidadeDeReceitas(usuario.Id);

            var usuarioJson = _mapper.Map<RespostaUsuarioConectadoJson>(usuario);
            usuarioJson.QuantidadeReceitas = quantidadeReceitas;

            return usuarioJson;
        });

        return await Task.WhenAll(tarefas);
    }

}
