public class Partida
{
    public int Numero { get; set; }
    public List<Jogador> Time1 { get; set; }
    public List<Jogador> Time2 { get; set; }
    public int GolsTime1 { get; set; }
    public int GolsTime2 { get; set; }

    public Partida(int numero, List<Jogador> time1, List<Jogador> time2, int gols1, int gols2)
    {
        Numero = numero;
        Time1 = time1;
        Time2 = time2;
        GolsTime1 = gols1;
        GolsTime2 = gols2;
    }

    public List<Jogador> Vencedor()
    {
        if (GolsTime1 > GolsTime2) return Time1;
        if (GolsTime2 > GolsTime1) return Time2;
        return null; // empate
    }

    public override string ToString()
    {
        return $"Partida {Numero}: {GolsTime1} x {GolsTime2}";
    }
}