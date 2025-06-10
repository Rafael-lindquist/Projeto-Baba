using interfaces;

namespace Models;

public class Jogador: IJogador
{
    public string RA { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Posicao { get; set; }
}