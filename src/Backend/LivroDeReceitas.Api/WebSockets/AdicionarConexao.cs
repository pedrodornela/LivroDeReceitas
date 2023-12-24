using LivroDeReceitas.Application.UseCases.Conexao.AceitarConexao;
using LivroDeReceitas.Application.UseCases.Conexao.GerarQRCode;
using LivroDeReceitas.Application.UseCases.Conexao.QRCodeLido;
using LivroDeReceitas.Application.UseCases.Conexao.RecusarConexao;
using LivroDeReceitas.Comunicacao.Response;
using LivroDeReceitas.Domain.Entidades;
using LivroDeReceitas.Exceptions;
using LivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.WebSockets;

[Authorize(Policy = "UsuarioLogado")]
public class AdicionarConexao : Hub
{
    private readonly Broadcaster _broadcaster;

    private readonly IAceitarConexaoUseCase _aceitarConexaoUseCase;
    private readonly IRecusarConexaoUseCase _recusarConexaoUseCase;
    private readonly IQRCodeLidoUseCase _qrCodeLidoUseCase;
    private readonly IGerarQRCodeUseCase _gerarQRCodeUseCase;
    private readonly IHubContext<AdicionarConexao> _hubContext;

    public AdicionarConexao(IHubContext<AdicionarConexao> hubContext, 
        IGerarQRCodeUseCase gerarQRCodeUseCase, 
        IQRCodeLidoUseCase qrCodeLidoUseCase,
        IRecusarConexaoUseCase recusarConexaoUseCase,
        IAceitarConexaoUseCase aceitarConexaoUseCase)
    {
        _broadcaster = Broadcaster.Instance;
        
        _gerarQRCodeUseCase = gerarQRCodeUseCase;
        _hubContext = hubContext;
        _qrCodeLidoUseCase = qrCodeLidoUseCase;
        _recusarConexaoUseCase = recusarConexaoUseCase;
        _aceitarConexaoUseCase = aceitarConexaoUseCase;
    }

    public async Task GetQRCode()
    {
        try
        {
            (var qrCode, var idUsuario) = await _gerarQRCodeUseCase.Executar();

            _broadcaster.InicializarConexao(_hubContext, idUsuario, Context.ConnectionId);

            await Clients.Caller.SendAsync("ResultadoQRCode", qrCode);

        }
        catch (LivroDeReceitasException exp)
        {
            await Clients.Caller.SendAsync("Erro", exp.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }

    public async Task QRCodeLido(string codigoConexao)
    {
        try
        {
            (RespostaUsuarioConexaoJson usuarioParaSeConectar, string idUsuarioQueGerouQRCode) = await _qrCodeLidoUseCase.Executar(codigoConexao);

            var connectionId = _broadcaster.GetConnectionIdDoUsuario(idUsuarioQueGerouQRCode);

            _broadcaster.ResetarTempoExpiracao(connectionId);
            _broadcaster.SetConnectionIdUsuarioLeitorQRCode(idUsuarioQueGerouQRCode, Context.ConnectionId);

            await Clients.Client(connectionId).SendAsync("ResultadoQRCode", usuarioParaSeConectar);

        }
        catch (LivroDeReceitasException exp)
        {
            await Clients.Caller.SendAsync("Erro", exp.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }


    public async Task RecusarConexao()
    {
        try
        {
            var connectionIdUsuarioQueGerouQRCode = Context.ConnectionId;

            var usuarioId = await _recusarConexaoUseCase.Executar();

            var connectionIdUsuarioQueLeuQRCode = _broadcaster.Remover(connectionIdUsuarioQueGerouQRCode, usuarioId);

            await Clients.Client(connectionIdUsuarioQueLeuQRCode).SendAsync("OnConexaoRecusada");

        }
        catch (LivroDeReceitasException exp)
        {
            await Clients.Caller.SendAsync("Erro", exp.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }

    public async Task AceitarConexao(string idUsuarioParaSeConectar)
    {
        try
        {
            var usuarioId = await _aceitarConexaoUseCase.Executar(idUsuarioParaSeConectar);

            var connectionIdUsuarioQueGerouQRCode = Context.ConnectionId;


            var connectionIdUsuarioQueLeuQRCode = _broadcaster.Remover(connectionIdUsuarioQueGerouQRCode, usuarioId);

            await Clients.Client(connectionIdUsuarioQueLeuQRCode).SendAsync("OnConexaoAceita");

        }
        catch (LivroDeReceitasException exp)
        {
            await Clients.Caller.SendAsync("Erro", exp.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }

}
