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
            // As novas propriedades terão seus valores padrão (null, false, listas vazias)
        };
        partidas.Add(partida);
        SalvarNoArquivo();
    }

    public List<Partida> BuscarPartida() => partidas;

    public Partida? ObterPartidaPorId(int id)
    {
        return partidas.FirstOrDefault(p => p.Id == id);
    }

    public bool AdicionarInteressado(int idPartida, string raJogador)
    {
        var partida = partidas.FirstOrDefault(p => p.Id == idPartida);
        if (partida == null)
            return false;

        if (partida.InteressadosRA.Contains(raJogador))
            return false;

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

    // Novo método para associar times gerados à partida
    public bool AssociarTimesAPartida(int idPartida, Time time1, Time time2)
    {
        var partida = ObterPartidaPorId(idPartida);
        if (partida == null) return false;

        partida.Time1Nome = time1.Nome;
        partida.Time1JogadoresRA = new List<string>(time1.JogadoresRA); // Copia a lista
        partida.Time2Nome = time2.Nome;
        partida.Time2JogadoresRA = new List<string>(time2.JogadoresRA); // Copia a lista
        
        // Reseta placares e resultado caso os times sejam reassociados
        partida.PlacarTime1 = null;
        partida.PlacarTime2 = null;
        partida.PartidaFinalizada = false;
        partida.NomeTimeVencedor = null;

        SalvarNoArquivo();
        return true;
    }

    // Novo método para registrar o resultado de uma partida
    public bool RegistrarResultado(int idPartida, int placarTime1, int placarTime2)
    {
        var partida = ObterPartidaPorId(idPartida);
        if (partida == null || partida.Time1JogadoresRA.Count == 0 || partida.Time2JogadoresRA.Count == 0)
        {
            // Partida não encontrada ou times não associados
            return false; 
        }

        partida.PlacarTime1 = placarTime1;
        partida.PlacarTime2 = placarTime2;
        partida.PartidaFinalizada = true;

        if (placarTime1 > placarTime2)
        {
            partida.NomeTimeVencedor = partida.Time1Nome;
        }
        else if (placarTime2 > placarTime1)
        {
            partida.NomeTimeVencedor = partida.Time2Nome;
        }
        else
        {
            partida.NomeTimeVencedor = "Empate";
        }

        SalvarNoArquivo();
        return true;
    }

    // Removed duplicate method as it conflicts with the existing implementation.
}