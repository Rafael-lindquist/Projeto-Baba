namespace interfaces;

public interface ITime
{
    // Propriedades essenciais
    string Nome { get; set; }
    int Id { get; }
    List<string> JogadoresRA { get; }
    bool Equals(object? obj);
    int GetHashCode();
}
