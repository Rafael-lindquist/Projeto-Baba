using Data;
using Services;

class Program
{
    static void Main()
    {
        using var db = new FutebolContext();
        db.Database.EnsureCreated();

        var service = new JogadorService(db);

        while (true)
        {
            Console.WriteLine("\n1. Adicionar jogador\n2. Listar jogadores\n3. Atualizar jogador\n4. Remover jogador\n5. Sair");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("RA: "); string ra = Console.ReadLine();
                    Console.Write("Nome: "); string nome = Console.ReadLine();
                    Console.Write("Idade: "); int idade = int.Parse(Console.ReadLine());
                    Console.Write("Posição (goleiro/defesa/ataque): "); string posicao = Console.ReadLine();
                    service.AdicionarJogador(ra, nome, idade, posicao);
                    Console.WriteLine("Jogador adicionado.");
                    break;

                case "2":
                    var jogadores = service.ListarJogadores();
                    if (jogadores.Count == 0)
                        Console.WriteLine("Nenhum jogador cadastrado.");
                    else
                        jogadores.ForEach(j => Console.WriteLine($"RA: {j.RA}, Nome: {j.Nome}, Idade: {j.Idade}, Posição: {j.Posicao}"));
                    break;

                case "3":
                    Console.Write("RA do jogador: "); string raAtual = Console.ReadLine();
                    Console.Write("Novo nome: "); string nomeAtual = Console.ReadLine();
                    Console.Write("Nova idade: "); int idadeAtual = int.Parse(Console.ReadLine());
                    Console.Write("Nova posição: "); string posicaoAtual = Console.ReadLine();
                    if (service.AtualizarJogador(raAtual, nomeAtual, idadeAtual, posicaoAtual))
                        Console.WriteLine("Jogador atualizado.");
                    else
                        Console.WriteLine("Jogador não encontrado.");
                    break;

                case "4":
                    Console.Write("RA do jogador: "); string raDel = Console.ReadLine();
                    if (service.RemoverJogador(raDel))
                        Console.WriteLine("Jogador removido.");
                    else
                        Console.WriteLine("Jogador não encontrado.");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}