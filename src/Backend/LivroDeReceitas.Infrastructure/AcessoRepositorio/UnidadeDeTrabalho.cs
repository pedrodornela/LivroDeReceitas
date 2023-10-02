using LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio;

public sealed class UnidadeDeTrabalho : IDisposable, IUnidadeDeTrabalho
{
    private readonly LivroDeReceitasContext _context;
    private bool _disposed;
    public UnidadeDeTrabalho(LivroDeReceitasContext contexto)
    {
        _context = contexto;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if(!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }

}
