using LivroDeReceitas.Comunicacao.Response;

namespace LivroDeReceitas.Application.UseCases.Conexao.QRCodeLido;
public interface IQRCodeLidoUseCase
{
    Task<(RespostaUsuarioConexaoJson usuarioParaSeConectar, string idUsuarioQueGerouQRCode)> Executar(string codigoConexao);
}
