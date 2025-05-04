using classes;

public class Sistema
{
    private List<Jogador> jogadores = new();
    private List<Jogo> jogos = new();
    private int proxCodigoJogador = 1;
    private int proxIdJogo = 1;

    // Jogador CRUD
    public void AdicionarJogador(string nome, int idade, Posicao posicao)
    {
        jogadores.Add(new Jogador(proxCodigoJogador++, nome, idade, posicao));
    }

    public List<Jogador> ListarJogadores() => jogadores;

    public void RemoverJogador(int codigo)
    {
        jogadores.RemoveAll(j => j.Codigo == codigo);
    }

    public void AtualizarJogador(int codigo, string nome, int idade, Posicao posicao)
    {
        var jogador = jogadores.FirstOrDefault(j => j.Codigo == codigo);
        if (jogador != null)
        {
            jogador.Nome = nome;
            jogador.Idade = idade;
            jogador.Posicao = posicao;
        }
    }

    // Jogo CRUD
    public void AdicionarJogo(DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? maxJogadores = null)
    {
        jogos.Add(new Jogo(proxIdJogo++, data, local, tipoCampo, jogadoresPorTime, maxJogadores));
    }

    public List<Jogo> ListarJogos() => jogos;

    public void RemoverJogo(int id)
    {
        jogos.RemoveAll(j => j.Id == id);
    }

    public void RegistrarInteressado(int idJogo, int codigoJogador)
    {
        var jogo = jogos.FirstOrDefault(j => j.Id == idJogo);
        var jogador = jogadores.FirstOrDefault(j => j.Codigo == codigoJogador);
        if (jogo != null && jogador != null && !jogo.Interessados.Contains(jogador))
        {
            if (jogo.MaxJogadores == null || jogo.Interessados.Count < jogo.MaxJogadores)
            {
                jogo.Interessados.Add(jogador);
            }
        }
    }
}