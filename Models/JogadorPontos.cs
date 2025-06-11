using interfaces;

namespace Models;
public abstract class JogadorPontos : IJogador
{
    public int Pontos { get; set; }
    string IJogador.RA { get; set;}
    string IJogador.Nome { get; set;}
    int IJogador.Idade { get; set;}
    string IJogador.Posicao { get; set;}

    public abstract void AtualizarPontuacao(string resultado); // "vitoria", "empate", "derrota"
}
