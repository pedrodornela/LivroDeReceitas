﻿namespace LivroDeReceitas.Domain.Repositorios.Receita;
public interface IReceitaWriteOnlyRepositorio
{
    Task Registrar(Entidades.Receita receita);
}
