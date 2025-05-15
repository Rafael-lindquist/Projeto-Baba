using classes;

class Program
{
    static void Main()
    {
        var sistema = new Sistema();

        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Adicionar Jogador");
            Console.WriteLine("2. Listar Jogadores");
            Console.WriteLine("3. Adicionar Jogo");
            Console.WriteLine("4. Listar Jogos");
            Console.WriteLine("5. Registrar Interessado");
            Console.WriteLine("6. Iniciar Partidas");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Idade: ");
                    int idade = int.Parse(Console.ReadLine());

                    Console.WriteLine("Posição (0-Goleiro, 1-Defesa, 2-Ataque): ");
                    int posInt = int.Parse(Console.ReadLine());
                    Posicao posicao = (Posicao)posInt;

                    sistema.AdicionarJogador(nome, idade, posicao);
                    Console.WriteLine("Jogador adicionado com sucesso!");
                    break;

                case "2":
                    var jogadores = sistema.ListarJogadores();
                    if (jogadores.Count == 0)
                        Console.WriteLine("Nenhum jogador cadastrado.");
                    else
                        jogadores.ForEach(j => Console.WriteLine(j));
                    break;

                case "3":
                    Console.Write("Data (dd/mm/aaaa): ");
                    DateTime data = DateTime.Parse(Console.ReadLine());

                    Console.Write("Local: ");
                    string local = Console.ReadLine();

                    Console.Write("Tipo de campo: ");
                    string tipo = Console.ReadLine();

                    Console.Write("Jogadores por time (incluindo goleiro): ");
                    int jogadoresPorTime = int.Parse(Console.ReadLine());

                    Console.Write("Máximo de jogadores (opcional - pressione Enter para ignorar): ");
                    string maxInput = Console.ReadLine();
                    int? maxJogadores = string.IsNullOrWhiteSpace(maxInput) ? null : int.Parse(maxInput);

                    sistema.AdicionarJogo(data, local, tipo, jogadoresPorTime, maxJogadores);
                    Console.WriteLine("Jogo adicionado!");
                    break;

                case "4":
                    var jogos = sistema.ListarJogos();
                    if (jogos.Count == 0)
                        Console.WriteLine("Nenhum jogo cadastrado.");
                    else
                        foreach (var jogo in jogos)
                        {
                            Console.WriteLine(jogo);
                            Console.WriteLine("Pode confirmar? " + (jogo.PodeConfirmarPartida() ? "Sim" : "Não"));
                        }
                    break;

                case "5":
                    Console.Write("ID do Jogo: ");
                    int idJogo = int.Parse(Console.ReadLine());

                    Console.Write("Código do Jogador: ");
                    int codJogador = int.Parse(Console.ReadLine());

                    sistema.RegistrarInteressado(idJogo, codJogador);
                    Console.WriteLine("Interesse registrado!");
                    break;

                case "6":
                    Console.Write("ID do Jogo: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Modo (1 - Quem ganha fica | 2 - Rodízio): ");
                    int modo = int.Parse(Console.ReadLine());
                    sistema.IniciarPartidasSimples(id, modo);
                    break;

                case "7":
                    Console.WriteLine("Encerrando...");
                    return;
                case "7":
                    Console.Write("ID do Jogo: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Modo (1 - Quem ganha fica | 2 - Rodízio): ");
                    int modo = int.Parse(Console.ReadLine());
                    sistema.IniciarPartidasSimples(id, modo);
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}