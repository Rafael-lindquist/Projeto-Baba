namespace classes;



public class Jogador
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public Posicao Posicao { get; set; }

    public Jogador(int codigo, string nome, int idade, Posicao posicao)
    {
        Codigo = codigo;
        Nome = nome;
        Idade = idade;
        Posicao = posicao;
    }

    public override string ToString()
    {
        return $"[{Codigo}] {Nome} - {Idade} anos - {Posicao}";
    }
}