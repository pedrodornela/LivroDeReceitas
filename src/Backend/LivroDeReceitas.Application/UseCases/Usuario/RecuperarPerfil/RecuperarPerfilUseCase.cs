using AutoMapper;
using LivroDeReceitas.Application.Servicos.UsuarioLogado;
using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
public class RecuperarPerfilUseCase : IRecuperarPerfilUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;

    public RecuperarPerfilUseCase(IMapper mapper, IUsuarioLogado usuarioLogado)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }
    public async Task<RespostaPerfilUsuarioJson> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        return _mapper.Map<RespostaPerfilUsuarioJson>(usuarioLogado);
    }
}
