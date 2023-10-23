using LivroDeReceitas.Application.Servicos.Criptografia;
using LivroDeReceitas.Application.Servicos.Token;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Repositorios;
using LivroDeReceitas.Exceptions.ExceptionsBase;

namespace LivroDeReceitas.Application.UseCases.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly UsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public LoginUseCase(UsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<RespostaLoginJson> Executar(RequisicaoLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmailSenha(request.Email, senhaCriptografada);

        if(usuario == null)
        {
            throw new LoginInvalidoException();
        }

        return new RespostaLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(usuario.Email)
        };

    }
}
