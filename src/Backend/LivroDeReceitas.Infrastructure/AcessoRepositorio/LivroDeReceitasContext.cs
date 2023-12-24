using LivroDeReceitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio;

public class LivroDeReceitasContext : DbContext
{
    public LivroDeReceitasContext(DbContextOptions<LivroDeReceitasContext> options) : base(options)
    { 
        
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Receita> Receitas { get; set; }
    public DbSet<Codigos> Codigos { get; set; }
    public DbSet<Conexao> Conexoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LivroDeReceitasContext).Assembly); 
    }



}
