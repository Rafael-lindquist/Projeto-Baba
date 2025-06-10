namespace interfaces;

public interface IPartida
{
    int Id { get; set; }
    DateTime Data { get; set; }
    string Local { get; set; }
    string TipoCampo { get; set; }
    int JogadoresPorTime { get; set; }
    int? LimiteTimes { get; set; }

    List<string> InteressadosRA { get; }
    bool PartidaConfirmada { get; }

    string Time1Nome { get; set; }
    List<string> Time1JogadoresRA { get; }
    int? PlacarTime1 { get; set; }

    string Time2Nome { get; set; }
    List<string> Time2JogadoresRA { get; }
    int? PlacarTime2 { get; set; }

    bool PartidaFinalizada { get; set; }
    string NomeTimeVencedor { get; set; }

}
