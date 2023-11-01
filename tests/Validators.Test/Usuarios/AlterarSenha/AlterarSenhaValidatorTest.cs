using FluentAssertions;
using LivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using LivroDeReceitas.Application.UseCases.Usuario.Registrar;
using LivroDeReceitas.Exceptions;
using UtilitarioParaOsTests.Requisicoes;
using Xunit;

namespace Validators.Test.Usuarios.AlterarSenha;

public class AlterarSenhaValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
    }

    [Theory]
    // Testes separados para o valor do tamanhoSenha
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(4)]
    public void Validar_Erro_SenhaInvalida(int tamanhoSenha)
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));

    }

    [Fact]
    public void Validar_Erro_SenhaVazio()
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        requisicao.NovaSenha = string.Empty;

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));

    }


}
