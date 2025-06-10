using interfaces;

namespace Models;


public class Partida : IPartida
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Local { get; set; }
    public string TipoCampo { get; set; }
    public int JogadoresPorTime { get; set; }
    public int? LimiteTimes { get; set; }
    public List<string> InteressadosRA { get; set; } = new();

    // Novas propriedades para gestão de times e resultados
    public string Time1Nome { get; set; }
    public List<string> Time1JogadoresRA { get; set; } = new List<string>();
    public int? PlacarTime1 { get; set; }

    public string Time2Nome { get; set; }
    public List<string> Time2JogadoresRA { get; set; } = new List<string>();
    public int? PlacarTime2 { get; set; }

    public bool PartidaFinalizada { get; set; } = false;
    public string NomeTimeVencedor { get; set; } // Armazena o nome do time vencedor ou "Empate"

    public bool PartidaConfirmada =>
        InteressadosRA.Count >= 2 * JogadoresPorTime;
}