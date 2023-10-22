namespace UtilitarioParaOsTests.Criptografia;
using LivroDeReceitas.Application.Servicos.Criptografia;
public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instancia()
    {
        return new EncriptadorDeSenha("ABCD123");
    }

}
