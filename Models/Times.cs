namespace Models;

public class Time{
    
    public string Nome { get; set; }
    public List<string> JogadoresRA { get; set; } = new List<string>();
    public int Id { get; set; }
    

    public override bool Equals(object? obj) {

        if (obj is Time outro)
        {
            return Id == outro.Id;
        }
        return false;
    }

    public override int GetHashCode() {

        return Id.GetHashCode();
    }

}

    
