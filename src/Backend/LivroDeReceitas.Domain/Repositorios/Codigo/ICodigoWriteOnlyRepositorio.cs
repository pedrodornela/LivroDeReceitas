﻿namespace LivroDeReceitas.Domain.Repositorios.Codigo;
public interface ICodigoWriteOnlyRepositorio
{
    Task Registrar(Domain.Entidades.Codigos codigo);
    Task Deletar(long usuarioId);
}
