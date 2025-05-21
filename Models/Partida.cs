namespace Models;


public class Partida
{
    public int Id { get; set; } 
    public DateTime Data { get; set; }
    public string Local { get; set; }
    public string TipoCampo { get; set; }
    public int JogadoresPorTime { get; set; }
    public int? LimiteTimes { get; set; }
    public List<string> InteressadosRA { get; set; } = new();
    
    public bool PartidaConfirmada =>
        InteressadosRA.Count >= 2 * JogadoresPorTime;
}
