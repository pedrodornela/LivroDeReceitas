﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LivroDeReceitas.Application.Servicos.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tempoDeValidadeDoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;
    
    public TokenController(double tempoDeValidadeDoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeValidadeDoTokenEmMinutos = tempoDeValidadeDoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, emailDoUsuario)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeValidadeDoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);

    }

    public void ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false
        };

        tokenHandler.ValidateToken(token, parametrosValidacao, out _);
    }


    private SymmetricSecurityKey SimetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }

}
