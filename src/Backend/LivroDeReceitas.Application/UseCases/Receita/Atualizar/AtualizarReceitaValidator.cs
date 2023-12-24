using FluentValidation;
using LivroDeReceitas.Comunicacao.Request;

namespace LivroDeReceitas.Application.UseCases.Receita.Atualizar;
public class AtualizarReceitaValidator : AbstractValidator<RequisicaoReceitaJson>
{
    public AtualizarReceitaValidator()
    {
        RuleFor(x => x).SetValidator(new ReceitaValidator());

    }
}
