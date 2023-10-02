using LivroDeReceitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LivroDeReceitas.Infrastructure.AcessoRepositorio;

public class LivroDeReceitasContext : DbContext
{
    public LivroDeReceitasContext(DbContextOptions<LivroDeReceitasContext> options) : base(options)
    { 
        
    }

    public DbSet<Usuario> Usuarios { get; set; }     

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LivroDeReceitasContext).Assembly); 
    }



}
