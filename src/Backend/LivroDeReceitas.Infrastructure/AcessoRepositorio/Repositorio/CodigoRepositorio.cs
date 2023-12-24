using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios.Codigo;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
public class CodigoRepositorio : ICodigoWriteOnlyRepositorio, ICodigoReadOnlyRepositorio
{
    private readonly LivroDeReceitasContext _contexto;
    public CodigoRepositorio(LivroDeReceitasContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Deletar(long usuarioId)
    {
        var codigos = await _contexto.Codigos.Where(c => c.UsuarioId == usuarioId).ToListAsync();

        if(codigos.Any())
        {
            _contexto.Codigos.RemoveRange(codigos);
        }
    }

    public async Task<Codigos> RecuperarEntidadeCodigo(string codigo)
    {
        return await _contexto.Codigos.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Codigo == codigo);
    }

    public async Task Registrar(Codigos codigo)
    {
        var codigoBancoDeDados = _contexto.Codigos.FirstOrDefault(c => c.UsuarioId == codigo.UsuarioId);

        if(codigoBancoDeDados is not null)
        {
            codigoBancoDeDados.Codigo = codigo.Codigo;
            _contexto.Codigos.Update(codigoBancoDeDados);
        }
        else
        {
            await _contexto.Codigos.AddAsync(codigo);
        }

    }
}
