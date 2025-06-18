using Models;
using System.Text.Json;

namespace Services;

public class JogadorService
{
    private static readonly string FilePath = Path.Combine(
     Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
     "jogadores.json");
    private List<Jogador> jogadores;

    public JogadorService()
    {
        jogadores = CarregarDoArquivo();
    }

    private List<Jogador> CarregarDoArquivo()
    {
        if (!File.Exists(FilePath))
            return new List<Jogador>();

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Jogador>>(json) ?? new List<Jogador>();
    }

    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(jogadores, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public void AdicionarJogador(string nome, int idade, string posicao, int pontos=0)
    {
        int maiorRA = jogadores
        .Select(j => int.Parse(j.RA))
        .DefaultIfEmpty(0)
        .Max();

        string ra = (maiorRA + 1).ToString("D3");

        jogadores.Add(new Jogador { RA = ra, Nome = nome, Idade = idade, Posicao = posicao, Pontos = pontos });
        SalvarNoArquivo();
    }

    public List<Jogador> ListarJogadores()
    {
        return jogadores;
    }

    public bool AtualizarJogador(string ra, string nome, int idade, string posicao, int pontos = 0)
    {
        var jogador = jogadores.FirstOrDefault(j => j.RA == ra);
        if (jogador == null) return false;

        jogador.Nome = nome;
        jogador.Idade = idade;
        jogador.Posicao = posicao;
        jogador.Pontos = pontos;
        SalvarNoArquivo();
        return true;
    }

    public bool RemoverJogador(string ra)
    {
        var jogador = jogadores.FirstOrDefault(j => j.RA == ra);
        if (jogador == null) return false;

        jogadores.Remove(jogador);
        SalvarNoArquivo();
        return true;
    }
    public Jogador ObterJogadorPorRA(string ra)
    {
        return jogadores.FirstOrDefault(j => j.RA == ra);
    }
    

    public void BuscarJogadorPorNome(string termo)
    {
        var encontrados = jogadores
            .Where(j => j.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (encontrados.Count == 0)
        {
            Console.WriteLine("❌ Nenhum jogador encontrado.");
            return;
        }

        Console.WriteLine($"🔎 Resultados para \"{termo}\":");
        foreach (var j in encontrados)
        {
            Console.WriteLine($"- RA: {j.RA} | Nome: {j.Nome} | Idade: {j.Idade} | Posição: {j.Posicao} | Pontos: {j.Pontos}");
        }
    }

}