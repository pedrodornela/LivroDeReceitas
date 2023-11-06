using LivroDeReceitas.Application.Servicos.Token;

namespace UtilitarioParaOsTests.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "TUU1eDQzamlIMztGRDU4QyoraFQobyIpKUs1LH5HVTtuQzI8ejpjJzRX");
    }

    public static TokenController TokenExpirado()
    {
        return new TokenController(0.01666, "TUU1eDQzamlIMztGRDU4QyoraFQobyIpKUs1LH5HVTtuQzI8ejpjJzRX");
    }

}
