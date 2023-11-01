using FluentAssertions;
using LivroDeReceitas.Application.UseCases.Usuario.Registrar;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using UtilitarioParaOsTests.Criptografia;
using UtilitarioParaOsTests.Mapper;
using UtilitarioParaOsTests.Repositorios;
using UtilitarioParaOsTests.Requisicoes;
using UtilitarioParaOsTests.Token;
using Xunit;

namespace UseCase.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task Validar_Erro_EmailJaRegistrado()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase(requisicao.Email);

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));

    }

    [Fact]
    public async Task Validar_Erro_EmailVazio()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = string.Empty;

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));

    }

    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }


}
