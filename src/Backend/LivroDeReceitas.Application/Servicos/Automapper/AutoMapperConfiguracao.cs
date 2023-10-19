using AutoMapper;
using LivroDeReceitas.Comunicacao.Request;

namespace LivroDeReceitas.Application.Servicos.Automapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao() 
    {
        CreateMap<Comunicacao.Request.RequestRegistrarUsuarioJson, Domain.Entidades.Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

    }
} 
