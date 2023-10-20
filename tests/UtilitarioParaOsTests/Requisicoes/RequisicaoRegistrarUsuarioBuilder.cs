using Bogus;
using LivroDeReceitas.Comunicacao.Request;

namespace UtilitarioParaOsTests.Requisicoes;

public class RequisicaoRegistrarUsuarioBuilder
{
    public static RequestRegistrarUsuarioJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarUsuarioJson>()
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!",$"{f.Random.Int(min: 1, max: 9)}"));

    }

}
