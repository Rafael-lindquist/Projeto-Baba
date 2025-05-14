using Data;
using Models;

namespace Services;

public class JogadorService
{
    private readonly FutebolContext _context;

    public JogadorService(FutebolContext context)
    {
        _context = context;
    }

    public void AdicionarJogador(string ra, string nome, int idade, string posicao)
    {
        var jogador = new Jogador { RA = ra, Nome = nome, Idade = idade, Posicao = posicao };
        _context.Jogadores.Add(jogador);
        _context.SaveChanges();
    }

    public List<Jogador> ListarJogadores()
    {
        return _context.Jogadores.ToList();
    }

    public bool AtualizarJogador(string ra, string nome, int idade, string posicao)
    {
        var jogador = _context.Jogadores.Find(ra);
        if (jogador == null) return false;

        jogador.Nome = nome;
        jogador.Idade = idade;
        jogador.Posicao = posicao;
        _context.SaveChanges();
        return true;
    }

    public bool RemoverJogador(string ra)
    {
        var jogador = _context.Jogadores.Find(ra);
        if (jogador == null) return false;

        _context.Jogadores.Remove(jogador);
        _context.SaveChanges();
        return true;
    }
}