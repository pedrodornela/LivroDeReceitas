using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

public class UsuarioRepositorio : UsuarioReadOnlyRepositorio, UsuarioWriteOnlyRepositorio
{
    private readonly LivroDeReceitasContext _context
;
    public UsuarioRepositorio(LivroDeReceitasContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Usuario usuario)
    {
       await  _context.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> ExisteUsuarioComEmail(string email)
    {
        return await _context.Usuarios.AnyAsync(c => c.Email.Equals(email));
    }
}
