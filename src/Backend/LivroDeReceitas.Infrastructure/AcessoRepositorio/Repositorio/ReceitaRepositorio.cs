using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios.Receita;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio, IReceitaReadOnlyRepositorio, IReceitaUpdateOnlyRepositorio
{
    private readonly LivroDeReceitasContext _context;
    public ReceitaRepositorio(LivroDeReceitasContext context)
    {
        _context = context;
    }

    async Task<Receita> IReceitaReadOnlyRepositorio.RecuperarPorId(long receitaId)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == receitaId);
    }

    async Task<Receita> IReceitaUpdateOnlyRepositorio.RecuperarPorId(long receitaId)
    {
        return await _context.Receitas
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == receitaId);
    }

    public async Task<IList<Receita>> RecuperarTodasDoUsuario(long usuarioId)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.Ingredientes)
            .Where(r => r.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<IList<Receita>> RecuperarTodasDosUsuarios(List<long> usuarioIds)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.Ingredientes)
            .Where(r => usuarioIds.Contains(r.UsuarioId)).ToListAsync();
    }

    public async Task Registrar(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }

    public void Update(Receita receita)
    {
        _context.Receitas.Update(receita);
    }

    public async Task Deletar(long receitaId)
    {
        var receita = await _context.Receitas.FirstOrDefaultAsync(r => r.Id == receitaId);

        _context.Receitas.Remove(receita);
    }

    public async Task<int> QuantidadeDeReceitas(long usuarioId)
    {
        return await _context.Receitas.CountAsync(r => r.UsuarioId == usuarioId);
    }
}
