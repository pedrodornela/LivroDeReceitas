using FluentAssertions;
using LivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using UtilitarioParaOsTests.Requisicoes;
using WebApi.Test.V1;
using Xunit;

namespace WebApi.Test.Usuario.Registrar;

public class RegistrarUsuarioTest : ControllerBase
{
    private const string METODO = "usuario";
    public RegistrarUsuarioTest(LivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
    { 
    
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_NomeVazio()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Nome = string.Empty;

        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));
    
    }

}
