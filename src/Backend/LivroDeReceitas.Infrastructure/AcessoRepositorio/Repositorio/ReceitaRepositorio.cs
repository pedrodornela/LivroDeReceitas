using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios.Receita;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio
{
    private readonly LivroDeReceitasContext _context
;
    public ReceitaRepositorio(LivroDeReceitasContext context)
    {
        _context = context;
    }
    public async Task Registrar(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }
}
