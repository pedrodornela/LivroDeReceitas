﻿using Bogus;
using LivroDeReceitas.Comunicacao.Request;
using LivroDeReceitas.Domain.Entidades;
using UtilitarioParaOsTests.Criptografia;

namespace UtilitarioParaOsTests.Entidades;

public class UsuarioBuilder
{
    public static (Usuario usuario, string senha) Construir()
    {
        string senha = string.Empty;

        var usuarioGerado = new Faker<Usuario>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f =>  
            {
                senha = f.Internet.Password();
                return EncriptadorDeSenhaBuilder.Instancia().Criptografar(senha);

            })
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));

        return (usuarioGerado, senha);
    }
}
