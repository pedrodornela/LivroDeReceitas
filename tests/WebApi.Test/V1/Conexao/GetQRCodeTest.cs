using LivroDeReceitas.Api.WebSockets;
using LivroDeReceitas.Application.UseCases.Conexao.GerarQRCode;
using Moq;
using Xunit;

namespace WebApi.Test.V1.Conexao;
public class GetQRCodeTest
{
    [Fact]
    public async Task Sucesso()
    {
        var useCaseGerarQRCode = GerarQRCodeBuilder();

        var hub = new AdicionarConexao(null, useCaseGerarQRCode, null, null, null);

        await hub.GetQRCode();

    }

    
    private IGerarQRCodeUseCase GerarQRCodeBuilder()
    {
        var useCaseMock = new Mock<IGerarQRCodeUseCase>();

        useCaseMock.Setup(c => c.Executar()).ReturnsAsync((Guid.NewGuid().ToString(), "IdUsuaio"));

        return useCaseMock.Object;
    }


}
