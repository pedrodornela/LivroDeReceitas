﻿namespace LivroDeReceitas.Domain.Repositorios;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Entidades.Usuario> RecuperarPorEmailSenha(string email, string senha);
    Task<Entidades.Usuario> RecuperarPorEmail(string email);

}
