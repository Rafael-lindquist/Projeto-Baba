using interfaces;

namespace Models;

public class Jogador : JogadorPontos
{
    public string RA { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Posicao { get; set; }
    public int Pontos { get; set; }

    public override void AtualizarPontuacao(string resultado)
    {
        switch (resultado.ToLower())
        {
            case "vitoria":
                Pontos += 3;
                break;
            case "empate":
                Pontos += 1;
                break;
            case "derrota":
                Pontos += 0;
                break;
        }
    }

}