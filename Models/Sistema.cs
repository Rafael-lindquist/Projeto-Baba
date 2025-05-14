namespace ProjetoBaba;

public class Sistema
{
    private List<Jogador> jogadores = new();
    private List<Jogo> jogos = new();
    private int proxCodigoJogador = 1;
    private int proxIdJogo = 1;

    public void AdicionarJogador(string nome, int idade, Posicao posicao)
    {
        jogadores.Add(new Jogador(proxCodigoJogador++, nome, idade, posicao));
    }

    public List<Jogador> ListarJogadores() => jogadores;

    public void AdicionarJogo(DateTime data, string local, string tipoCampo, int jogadoresPorTime, int? maxJogadores = null)
    {
        jogos.Add(new Jogo(proxIdJogo++, data, local, tipoCampo, jogadoresPorTime, maxJogadores));
    }

    public List<Jogo> ListarJogos() => jogos;

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
    public void IniciarPartidasSimples(int idJogo, int modo)
    {
        var jogo = jogos.FirstOrDefault(j => j.Id == idJogo);
        if (jogo == null || !jogo.PodeConfirmarPartida())
        {
            Console.WriteLine("Jogo inválido ou sem jogadores suficientes.");
            return;
        }

        var fila = new Queue<List<Jogador>>();
        var jogadores = jogo.Interessados.OrderBy(j => Guid.NewGuid()).ToList();
        int porTime = jogo.JogadoresPorTime;

        for (int i = 0; i + porTime <= jogadores.Count; i += porTime)
            fila.Enqueue(jogadores.GetRange(i, porTime));

        int num = 1;
        var time1 = fila.Dequeue();
        var time2 = fila.Dequeue();

        while (true)
        {
            Console.WriteLine($"\nPartida {num}:");
            Console.WriteLine($"Time 1: {string.Join(", ", time1.Select(j => j.Nome))}");
            Console.WriteLine($"Time 2: {string.Join(", ", time2.Select(j => j.Nome))}");

            Console.Write("Gols Time 1: ");
            int g1 = int.Parse(Console.ReadLine());

            Console.Write("Gols Time 2: ");
            int g2 = int.Parse(Console.ReadLine());

            var partida = new Partida(num++, time1, time2, g1, g2);
            jogo.Partidas.Add(partida);

            var vencedor = partida.Vencedor();

            if (modo == 1) // Quem ganha fica
            {
                var perdedor = vencedor == time1 ? time2 : time1;
                fila.Enqueue(perdedor);
                if (fila.Count == 0 || vencedor == null) break;
                time1 = vencedor;
                time2 = fila.Dequeue();
            }
            else if (modo == 2) // Rodízio simples
            {
                fila.Enqueue(time1);
                fila.Enqueue(time2);
                if (fila.Count < 2) break;
                time1 = fila.Dequeue();
                time2 = fila.Dequeue();
            }
        }

        Console.WriteLine("\nPartidas finalizadas.");
    }
}
