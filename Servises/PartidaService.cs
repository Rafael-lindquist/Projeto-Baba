using Models;
using System.Text.Json;

namespace Services;

public class PartidaService
{
    private static readonly string FilePath = Path.Combine(
        Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
        "partidas.json");
    private List<Partida> partidas;
    private int ultimoId;

    public PartidaService()
    {
        partidas = CarregarDoArquivo();
        ultimoId = partidas.Count > 0 ? partidas.Max(p => p.Id) : 0;
    }

    private List<Partida> CarregarDoArquivo()
    {
        if (!File.Exists(FilePath)) return new();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Partida>>(json) ?? new();
    }

    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(partidas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public void AdicionarPartida(DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? limiteTimes)
    {
        var partida = new Partida
        {
            Id = ++ultimoId,
            Data = data,
            Local = local,
            TipoCampo = tipoCampo,
            JogadoresPorTime = jogadoresPorTime,
            LimiteTimes = limiteTimes
        };
        partidas.Add(partida);
        SalvarNoArquivo();
    }

    public List<Partida> ListarPartidas() => partidas;

    public Partida? ObterPartidaPorId(int id)
    {
        return partidas.FirstOrDefault(p => p.Id == id);
    }

    public bool AdicionarInteressado(int idPartida, string raJogador)
    {
        var partida = partidas.FirstOrDefault(p => p.Id == idPartida);
        if (partida == null) 
            return false;

        // Não adiciona se já estiver inscrito
        if (partida.InteressadosRA.Contains(raJogador))
            return false;

        // Verifica limite de vagas
        if (partida.LimiteTimes.HasValue &&
            partida.InteressadosRA.Count >= partida.LimiteTimes.Value * partida.JogadoresPorTime)
            return false;

        partida.InteressadosRA.Add(raJogador);
        SalvarNoArquivo();
        return true;
    }

    public bool RemoverPartida(int id)
    {
        var partida = partidas.FirstOrDefault(p => p.Id == id);
        if (partida == null) 
            return false;
        partidas.Remove(partida);
        SalvarNoArquivo();
        return true;
    }

    public bool AtualizarPartida(int id, DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? limiteTimes)
    {
        var partida = partidas.FirstOrDefault(p => p.Id == id);
        if (partida == null) 
            return false;

        partida.Data = data;
        partida.Local = local;
        partida.TipoCampo = tipoCampo;
        partida.JogadoresPorTime = jogadoresPorTime;
        partida.LimiteTimes = limiteTimes;
        SalvarNoArquivo();
        return true;
    }
}
