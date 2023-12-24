using FluentValidation;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Domain.Extension;

namespace LivroDeReceitas.Application.UseCases.Receita;
public class ReceitaValidator : AbstractValidator<RequisicaoReceitaJson>
{
    public ReceitaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty();
        RuleFor(x => x.Categoria).IsInEnum();
        RuleFor(x => x.ModoPreparo).NotEmpty();
        RuleFor(x => x.Ingredientes).NotEmpty();
        RuleFor(x => x.TempoPreparo).InclusiveBetween(1, 1000);
        RuleForEach(x => x.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty();
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty();
        });

        RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto.RemoverAcentos().ToLower()).Distinct();

            if (produtosDistintos.Count() != ingredientes.Count)
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", "Ingredientes da Receita Repetidos"));
            }

        });

    }
}
