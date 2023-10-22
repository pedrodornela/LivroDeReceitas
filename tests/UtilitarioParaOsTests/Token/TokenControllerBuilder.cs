using LivroDeReceitas.Application.Servicos.Token;

namespace UtilitarioParaOsTests.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "Kz9sbUVWQjhkNS90SntAwqNkRXk1Pj9vTiIvNEc3Q2JsbHhiUj51QWIwTEpjeTpyaz1o");
    }

}
