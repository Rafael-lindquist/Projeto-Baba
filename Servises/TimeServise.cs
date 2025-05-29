using Models;
using Services;

public class TimeService
{
    private JogadorService jogadorService;

    public TimeService(JogadorService jogadorService)
    {
        this.jogadorService = jogadorService;
    }

    // Gera dois times pegando jogadores na ordem de chegada (lista de RAs)
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

    // Gera times equilibrados por posição
    public List<Time> GerarTimesEquilibrados(List<string> interessadosRA)
    {
        // Exemplo simples:
        // Divide jogadores em grupos por posição (goleiro, defesa, ataque)
        // Distribui equilibradamente entre os dois times

        var jogadores = interessadosRA.Select(ra => jogadorService.ObterJogadorPorRA(ra)).Where(j => j != null).ToList();

        var goleiros = jogadores.Where(j => j.Posicao.ToLower() == "goleiro").ToList();
        var defesas = jogadores.Where(j => j.Posicao.ToLower() == "defesa").ToList();
        var atacantes = jogadores.Where(j => j.Posicao.ToLower() == "ataque").ToList();

        if (goleiros.Count < 2)
            return null; // precisa de ao menos 2 goleiros para equilibrar

        var time1 = new Time { Nome = "Time 1", JogadoresRA = new List<string>() };
        var time2 = new Time { Nome = "Time 2", JogadoresRA = new List<string>() };

        // Distribuir goleiros
        time1.JogadoresRA.Add(goleiros[0].RA);
        time2.JogadoresRA.Add(goleiros[1].RA);

        // Distribuir defensores alternando
        for (int i = 0; i < defesas.Count; i++)
        {
            if (i % 2 == 0) time1.JogadoresRA.Add(defesas[i].RA);
            else time2.JogadoresRA.Add(defesas[i].RA);
        }

        // Distribuir atacantes alternando
        for (int i = 0; i < atacantes.Count; i++)
        {
            if (i % 2 == 0) time1.JogadoresRA.Add(atacantes[i].RA);
            else time2.JogadoresRA.Add(atacantes[i].RA);
        }

        return new List<Time> { time1, time2 };
    }

    internal IEnumerable<Time> GerarTimes()
    {
        throw new NotImplementedException();
    }
}