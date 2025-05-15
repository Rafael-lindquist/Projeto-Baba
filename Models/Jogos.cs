namespace classes;

public class Jogo
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Local { get; set; }
    public string TipoCampo { get; set; }
    public int JogadoresPorTime { get; set; }
    public int? MaxJogadores { get; set; }
    public List<Jogador> Interessados { get; set; } = new();
    public List<Partida> Partidas { get; set; } = new();

    public Jogo(int id, DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? maxJogadores = null)
    {
        Id = id;
        Data = data;
        Local = local;
        TipoCampo = tipoCampo;
        JogadoresPorTime = jogadoresPorTime;
        MaxJogadores = maxJogadores;
    }

    public bool PodeConfirmarPartida()
    {
        int timesPossiveis = Interessados.Count / JogadoresPorTime;
        return timesPossiveis >= 2;
    }

    public override string ToString()
    {
        return $"Jogo {Id} em {Data.ToShortDateString()} no {Local} - Campo: {TipoCampo}, {JogadoresPorTime} por time, Máx: {(MaxJogadores?.ToString() ?? "Ilimitado")}, Interessados: {Interessados.Count}";
    }
}