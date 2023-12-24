using Microsoft.AspNetCore.SignalR;
using System;
using System.Timers;

namespace LivroDeReceitas.Api.WebSockets;

public class Conexao
{
    private readonly IHubContext<AdicionarConexao> _hubContext;
    private readonly string UsuarioQueCriouQRCodeConnectionId;
    
    private Action<string> _callbackTempoExpirado;
    private string _connectionUsuarioLeitorQRCode;

    public Conexao(IHubContext<AdicionarConexao> hubContext, string usuarioQueCriouQRCodeConnectionId)
    {
        _hubContext = hubContext;
        UsuarioQueCriouQRCodeConnectionId = usuarioQueCriouQRCodeConnectionId;
    }

    private short _tempoRestanteEmSecundos {  get; set; }
    private Timer _timer {  get; set; }

    public void IniciarContagemTempo(Action<string> callbackTempoExpirdo)
    {
        _callbackTempoExpirado = callbackTempoExpirdo;

        StartTimer();
    }

    public void ResetarContagemTempo()
    {
        StopTimer();
        StartTimer();
    }

    private void StartTimer()
    {
        _tempoRestanteEmSecundos = 60;
        _timer = new Timer(1000)
        {
            Enabled = false
        };

        _timer.Elapsed += ElapsedTimer;
        _timer.Enabled = true;
    }

    public void StopTimer()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
    }

    public void SetConnectionIdUsuarioLeitorQRCode(string connectionId)
    {
        _connectionUsuarioLeitorQRCode = connectionId;
    }

    public string UsuarioQueLeuQRCode()
    {
        return _connectionUsuarioLeitorQRCode;
    }

    private async void ElapsedTimer(object sender, ElapsedEventArgs e)
    {
        if(_tempoRestanteEmSecundos >= 0)
        {
            await _hubContext.Clients.Client(UsuarioQueCriouQRCodeConnectionId).SendAsync("SetTempoRestante", _tempoRestanteEmSecundos--);
        }
        else
        {
            StopTimer();
            _callbackTempoExpirado(UsuarioQueCriouQRCodeConnectionId);
        }
    }




}
