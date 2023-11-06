using FluentAssertions;
using System.Net;
using UtilitarioParaOsTests.Requisicoes;
using UtilitarioParaOsTests.Token;
using Xunit;

namespace WebApi.Test.V1.Usuario.AlterarSenha;

public class AlterarSenhaTokenInvalido : ControllerBase
{
    private const string METODO = "usuario/alterar-senha";

    private LivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;
    public AlterarSenhaTokenInvalido(LivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    }


    [Fact]
    public async Task Validar_Eroo_TokenVazio()
    {
        var token = string.Empty;

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Validar_Eroo_TokenUsuarioFake()
    {
        var token = TokenControllerBuilder.Instancia().GerarToken("usuario@fake.com");

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Validar_Eroo_TokenExpirado()
    {
        var token = TokenControllerBuilder.TokenExpirado().GerarToken(_usuario.Email);
        Thread.Sleep(1000);

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

}
