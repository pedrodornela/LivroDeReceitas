using FluentAssertions;
using LivroDeReceitas.Application.UseCases.Login.FazerLogin;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using UtilitarioParaOsTests.Criptografia;
using UtilitarioParaOsTests.Entidades;
using UtilitarioParaOsTests.Repositorios;
using UtilitarioParaOsTests.Token;
using Xunit;

namespace UseCase.Test.Login.FazerLogin;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha )= UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);
        var resposta = await useCase.Executar(new LivroDeReceitas.Comunicacao.Request.RequisicaoLoginJson
        {
            Email = usuario.Email,
            Senha = senha
        });

        resposta.Should().NotBeNull();
        resposta.Nome.Should().Be(usuario.Nome);
        resposta.Token.Should().NotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task Validar_Erro_SenhaInvalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new LivroDeReceitas.Comunicacao.Request.RequisicaoLoginJson
            {
                Email = usuario.Email,
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_EmailInvalido()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new LivroDeReceitas.Comunicacao.Request.RequisicaoLoginJson
            {
                Email = "email@invalido.com",
                Senha = senha
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_SenhaInvalido()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new LivroDeReceitas.Comunicacao.Request.RequisicaoLoginJson
            {
                Email = "email@invalido.com",
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    private LoginUseCase CriarUseCase(LivroDeReceitas.Domain.Entidades.Usuario usuario)
    {
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().RecuperarPorEmailSenha(usuario).Construir();

        return new LoginUseCase(repositorioReadOnly, encriptador, token);
    }

}
