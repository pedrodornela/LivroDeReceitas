using System.Drawing;

namespace LivroDeReceitas.Application.UseCases.Conexao.GerarQRCode;
public interface IGerarQRCodeUseCase
{
    Task<(byte[] qrCode, string idUsuario)> Executar();
}
