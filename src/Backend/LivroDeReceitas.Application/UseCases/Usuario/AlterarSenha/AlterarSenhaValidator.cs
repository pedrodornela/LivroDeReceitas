using FluentValidation;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Exceptions;
using System.Text.RegularExpressions;

namespace LivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;

public class AlterarSenhaValidator : AbstractValidator<RequisicaoAlterarSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(c => c.NovaSenha).SetValidator(new SenhaValidator());
    }

}
