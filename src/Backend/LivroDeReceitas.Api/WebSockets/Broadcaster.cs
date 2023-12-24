using LivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;

namespace LivroDeReceitas.Api.WebSockets;

public class Broadcaster
{
    private readonly static Lazy<Broadcaster> _instance = new(() => new Broadcaster());

    public static Broadcaster Instance { get { return _instance.Value; } }

    private ConcurrentDictionary<string, object> _dictionary { get; set; }

    public Broadcaster()
    {
        _dictionary = new ConcurrentDictionary<string, object>();
    }

    public void InicializarConexao(IHubContext<AdicionarConexao> hubContext, string idUsuarioQueGerouQRCode, string connectionId)
    {
        var conexao = new Conexao(hubContext, connectionId);
        
        _dictionary.TryAdd(connectionId, conexao);
        _dictionary.TryAdd(idUsuarioQueGerouQRCode, connectionId);

        conexao.IniciarContagemTempo(CallBackTempoExpirado);
    }

    private void CallBackTempoExpirado(string connectionId)
    {
        _dictionary.TryRemove(connectionId, out _);
    }

    public string GetConnectionIdDoUsuario(string usuarioId)
    {
        if(!_dictionary.TryGetValue(usuarioId, out var connectionId))
        {
            throw new LivroDeReceitasException("");
        }

        return connectionId.ToString();

    }

    public void ResetarTempoExpiracao(string connectionId)
    {
        _dictionary.TryGetValue(connectionId, out var objetoConexao);

        var conexao = objetoConexao as Conexao;

        conexao.ResetarContagemTempo();
    }

    public void SetConnectionIdUsuarioLeitorQRCode(string idUsuarioQueGerouQRCode, string connectionIdUsuarioLeitorQRCode)
    {
        var connectionIdUsuarioQueLeuQRCode = GetConnectionIdDoUsuario(idUsuarioQueGerouQRCode);

        _dictionary.TryGetValue(connectionIdUsuarioQueLeuQRCode, out var objetoConexao);

        var conexao = objetoConexao as Conexao;

        conexao.SetConnectionIdUsuarioLeitorQRCode(connectionIdUsuarioLeitorQRCode);

    }

    public string Remover(string connectionId, string usuarioId)
    {
        _dictionary.TryGetValue(connectionId, out var objetoConexao);

        var conexao = objetoConexao as Conexao;

        conexao.StopTimer();

        _dictionary.TryRemove(connectionId, out _);
        _dictionary.TryRemove(usuarioId, out _);

        return conexao.UsuarioQueLeuQRCode();
    }
}
