﻿using LivroDeReceitas.Domain.Enum;

namespace LivroDeReceitas.Domain.Entidades;
public class Receita : EntidadeBase
{
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }
    public ICollection<Ingrediente> Ingredientes { get; set; }

}
