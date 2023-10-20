using FluentAssertions;
using LivroDeReceitas.Application.UseCases.Usuario.Registrar;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Exceptions;
using UtilitarioParaOsTests.Requisicoes;
using Xunit;

namespace Validators.Test.Usuarios.Registrar;

public class RegistrarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_NomeVazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Nome = string.Empty;

        var resultado = validator.Validate(requisicao); 

        
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));

    }


    [Fact]
    public void Validar_Erro_EmailVazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = string.Empty;

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));

    }


    [Fact]
    public void Validar_Erro_SenhaVazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Senha = string.Empty;

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));

    }


    [Fact]
    public void Validar_Erro_TelefoneVazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Telefone = string.Empty;

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO));

    }


    [Fact]
    public void Validar_Erro_EmailInvalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = "sff";

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));

    }

    [Fact]
    public void Validar_Erro_TelefoneInvalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Telefone = "31 9";

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));

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
         var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(requisicao);


        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));

    }

}
