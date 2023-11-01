using AutoMapper;
using LivroDeReceitas.Application.Servicos.Automapper;

namespace UtilitarioParaOsTests.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguracao>();
        });

        return configuracao.CreateMapper();
    }
}
