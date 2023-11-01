using Bogus;
using LivroDeReceitas.Comunicacao.Request;

namespace UtilitarioParaOsTests.Requisicoes;

public class RequisicaoAlterarSenhaUsuarioBuilder
{
    public static RequisicaoAlterarSenhaJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequisicaoAlterarSenhaJson>()
            .RuleFor(c => c.SenhaAtual, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.NovaSenha, f => f.Internet.Password(tamanhoSenha));
    }

}
