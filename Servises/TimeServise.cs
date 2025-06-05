using Models;
using Services;

public class TimeService
{
    private JogadorService jogadorService;

    public TimeService(JogadorService jogadorService)
    {
        this.jogadorService = jogadorService;
    }

    public List<Time> GerarTimesOrdemChegada(List<string> interessadosRA, int jogadoresPorTime)
    {
        if (interessadosRA.Count < jogadoresPorTime * 2)
            return null; // jogadores insuficientes

        var time1 = new Time { Nome = "Time 1", JogadoresRA = interessadosRA.Take(jogadoresPorTime).ToList() };
        var time2 = new Time { Nome = "Time 2", JogadoresRA = interessadosRA.Skip(jogadoresPorTime).Take(jogadoresPorTime).ToList() };

        return new List<Time> { time1, time2 };
    }

    // Gera times genéricos (exemplo: embaralha e divide)
    public List<Time> GerarTimesGenerico(List<string> interessadosRA, int jogadoresPorTime)
    {
        if (interessadosRA.Count < jogadoresPorTime * 2)
            return null;

        var random = new Random();
        var embaralhado = interessadosRA.OrderBy(x => random.Next()).ToList();

        var time1 = new Time { Nome = "Time 1", JogadoresRA = embaralhado.Take(jogadoresPorTime).ToList() };
        var time2 = new Time { Nome = "Time 2", JogadoresRA = embaralhado.Skip(jogadoresPorTime).Take(jogadoresPorTime).ToList() };

        return new List<Time> { time1, time2 };
    }

    
    public List<Time> GerarTimesEquilibrados(List<string> interessadosRA, int numeroMaximoJogadoresPorTime)
{
    var jogadores = interessadosRA.Select(ra => jogadorService.ObterJogadorPorRA(ra)).Where(j => j != null).ToList();

    var goleiros = jogadores.Where(j => j.Posicao.ToLower() == "goleiro").ToList();
    var defesas = jogadores.Where(j => j.Posicao.ToLower() == "defesa").ToList();
    var atacantes = jogadores.Where(j => j.Posicao.ToLower() == "ataque").ToList();

    if (goleiros.Count < 2)
        return null; // precisa de ao menos 2 goleiros para equilibrar

    var time1 = new Time { Nome = "Time 1", JogadoresRA = new List<string>() };
    var time2 = new Time { Nome = "Time 2", JogadoresRA = new List<string>() };

    // Distribuir goleiros (máximo 1 por time)
    time1.JogadoresRA.Add(goleiros[0].RA);
    time2.JogadoresRA.Add(goleiros[1].RA);

    // Combinar defensores e atacantes para distribuição equilibrada
    var restantes = defesas.Concat(atacantes).ToList();

    // Distribuir alternando para manter equilíbrio e respeitar o limite de jogadores
    for (int i = 0; i < restantes.Count; i++)
    {
        if (time1.JogadoresRA.Count < numeroMaximoJogadoresPorTime &&
            (time1.JogadoresRA.Count <= time2.JogadoresRA.Count))
        {
            time1.JogadoresRA.Add(restantes[i].RA);
        }
        else if (time2.JogadoresRA.Count < numeroMaximoJogadoresPorTime)
        {
            time2.JogadoresRA.Add(restantes[i].RA);
        }

        // Parar a distribuição se ambos os times atingirem o limite máximo de jogadores
        if (time1.JogadoresRA.Count == numeroMaximoJogadoresPorTime &&
            time2.JogadoresRA.Count == numeroMaximoJogadoresPorTime)
        {
            break;
        }
    }

    // Garantir que os times tenham o mesmo número de jogadores
    while (time1.JogadoresRA.Count > time2.JogadoresRA.Count)
    {
        var jogadorExtra = time1.JogadoresRA.Last();
        time1.JogadoresRA.Remove(jogadorExtra);
    }

    while (time2.JogadoresRA.Count > time1.JogadoresRA.Count)
    {
        var jogadorExtra = time2.JogadoresRA.Last();
        time2.JogadoresRA.Remove(jogadorExtra);
    }

    return new List<Time> { time1, time2 };
}

    internal IEnumerable<Time> GerarTimes()
    {
        throw new NotImplementedException();
    }
}