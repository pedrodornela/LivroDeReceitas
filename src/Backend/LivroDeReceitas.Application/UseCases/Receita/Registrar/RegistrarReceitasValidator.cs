using FluentValidation;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Domain.Entidades;

namespace LivroDeReceitas.Application.UseCases.Receita.Registrar;
public class RegistrarReceitasValidator : AbstractValidator<RequisicaoReceitaJson>
{
    public RegistrarReceitasValidator()
    {
        RuleFor(x => x).SetValidator(new ReceitaValidator());

    }
}
  