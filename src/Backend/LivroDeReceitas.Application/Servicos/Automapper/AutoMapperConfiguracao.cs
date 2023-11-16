using AutoMapper;
using HashidsNet;
using LivroDeReceitas.Comunicacao.Request;

namespace LivroDeReceitas.Application.Servicos.Automapper;

public class AutoMapperConfiguracao : Profile
{
    private readonly IHashids _hashids;

    public AutoMapperConfiguracao(IHashids hashids) 
    {
        _hashids = hashids;

        RequisicaoParaEntidade();
        EntidadeParaResposta();

    }

    private void RequisicaoParaEntidade()
    {
        CreateMap<Comunicacao.Request.RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

        CreateMap<Comunicacao.Request.RequisicaoReceitaJson, Domain.Entidades.Receita>();

        CreateMap<Comunicacao.Request.RequisicaoIngredienteJson, Domain.Entidades.Ingrediente>();
    }

    private void EntidadeParaResposta()
    {
        CreateMap<Domain.Entidades.Receita , Comunicacao.Response.RespostaReceitaJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));


        CreateMap<Domain.Entidades.Ingrediente, Comunicacao.Response.RespostaIngredienteJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));

        CreateMap<Domain.Entidades.Receita, Comunicacao.Response.RespostaReceitasDashboardJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)))
            .ForMember(destino => destino.QuantidadeIngredientes, config => config.MapFrom(origem => origem.Ingredientes.Count));
    }



} 
