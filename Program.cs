using Services;
using Models;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var jogadorService = new JogadorService();
        var partidaService = new PartidaService();
        var timeService = new TimeService(jogadorService);
        while (true)
        {
            Console.WriteLine("\n=== Menu Principal ===");
            Console.WriteLine("1. Gerenciar jogadores");
            Console.WriteLine("2. Gerenciar partidas");
            Console.WriteLine("3. Gerenciar times");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha: ");
            var escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    MenuJogadores(jogadorService);
                    break;
                case "2":
                    MenuPartidas(partidaService, jogadorService);
                    break;
                case "3":
                    MenuTimes(partidaService, jogadorService, timeService);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void MenuJogadores(JogadorService service)
    {
        while (true)
        {
            Console.WriteLine("\n--- Jogadores ---");
            Console.WriteLine("1. Adicionar jogador");
            Console.WriteLine("2. Listar jogadores");
            Console.WriteLine("3. Atualizar jogador");
            Console.WriteLine("4. Remover jogador");
            Console.WriteLine("5. Voltar");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Nome: "); string nome = Console.ReadLine();
                    Console.Write("Idade: "); int idade = int.Parse(Console.ReadLine());
                    Console.Write("Posição (goleiro/defesa/ataque): "); string posicao = Console.ReadLine();
                    service.AdicionarJogador(nome, idade, posicao);
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

    static void MenuPartidas(PartidaService service, JogadorService jogadorService)
    {
        while (true)
        {
            Console.WriteLine("\n--- Partidas ---");
            Console.WriteLine("1. Adicionar partida");
            Console.WriteLine("2. Listar partidas");
            Console.WriteLine("3. Atualizar partida");
            Console.WriteLine("4. Remover partida");
            Console.WriteLine("5. Adicionar interessado");
            Console.WriteLine("6. Voltar");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Data (dd/MM/yyyy): ");
                    DateTime data = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.Write("Local: "); string local = Console.ReadLine();
                    Console.Write("Tipo de campo: "); string tipo = Console.ReadLine();
                    Console.Write("Jogadores por time: "); int qtd = int.Parse(Console.ReadLine());
                    Console.Write("Limite de times (opcional - pressione Enter se não quiser): ");
                    string limiteInput = Console.ReadLine();
                    int? limite = string.IsNullOrWhiteSpace(limiteInput) ? null : int.Parse(limiteInput);
                    service.AdicionarPartida(data, local, tipo, qtd, limite);
                    Console.WriteLine("Partida adicionada.");
                    break;

                case "2":
                    var partidas = service.ListarPartidas();
                    if (partidas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma partida cadastrada.");
                    }
                    else
                    {
                        foreach (var p in partidas)
                        {
                            var nomesInteressados = p.InteressadosRA
                                .Select(ra =>
                                {
                                    var jogador = jogadorService.ListarJogadores().FirstOrDefault(j => j.RA == ra);
                                    return jogador != null ? jogador.Nome : $"RA {ra} (não encontrado)";
                                });

                            Console.WriteLine($"\nID: {p.Id}");
                            Console.WriteLine($"Data: {p.Data:dd/MM/yyyy}");
                            Console.WriteLine($"Local: {p.Local}");
                            Console.WriteLine($"Tipo de campo: {p.TipoCampo}");
                            Console.WriteLine($"Jogadores por time: {p.JogadoresPorTime}");
                            Console.WriteLine($"Limite de times: {(p.LimiteTimes.HasValue ? p.LimiteTimes.ToString() : "Sem limite")}");
                            Console.WriteLine($"Interessados: {string.Join(", ", nomesInteressados)}");
                            Console.WriteLine($"Status: {(p.PartidaConfirmada ? "Confirmada ✅" : "A confirmar ⏳")}");
                        }
                    }
                    break;

                case "3":
                    Console.Write("ID da partida: "); int idAtual = int.Parse(Console.ReadLine());
                    Console.Write("Nova data (dd/MM/yyyy): ");
                    DateTime novaData = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.Write("Novo local: "); string novoLocal = Console.ReadLine();
                    Console.Write("Novo tipo de campo: "); string novoTipo = Console.ReadLine();
                    Console.Write("Novo nº de jogadores por time: "); int novoQtd = int.Parse(Console.ReadLine());
                    Console.Write("Novo limite de times (pressione Enter para deixar igual): ");
                    string novoLimiteInput = Console.ReadLine();
                    int? novoLimite = string.IsNullOrWhiteSpace(novoLimiteInput) ? null : int.Parse(novoLimiteInput);
                    if (service.AtualizarPartida(idAtual, novaData, novoLocal, novoTipo, novoQtd, novoLimite))
                        Console.WriteLine("Partida atualizada.");
                    else
                        Console.WriteLine("Partida não encontrada.");
                    break;

                case "4":
                    Console.Write("ID da partida: "); int idDel = int.Parse(Console.ReadLine());
                    if (service.RemoverPartida(idDel))
                        Console.WriteLine("Partida removida.");
                    else
                        Console.WriteLine("Partida não encontrada.");
                    break;

                case "5":
                    Console.Write("ID da partida: "); int idInt = int.Parse(Console.ReadLine());
                    Console.Write("RA do jogador interessado: "); string raJogador = Console.ReadLine();

                    if (!jogadorService.ListarJogadores().Any(j => j.RA == raJogador))
                    {
                        Console.WriteLine("Jogador com esse RA não encontrado.");
                        break;
                    }

                    if (service.AdicionarInteressado(idInt, raJogador))
                        Console.WriteLine("Interessado adicionado.");
                    else
                        Console.WriteLine("Não foi possível adicionar (limite atingido, partida não encontrada ou jogador já interessado).");
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void MenuTimes(PartidaService partidaService, JogadorService jogadorService, TimeService timeService)
    {
        while (true)
        {
            Console.WriteLine("\n--- Gestão de Times ---");
            Console.WriteLine("1. Gerar times por ordem de chegada");
            Console.WriteLine("2. Gerar times equilibrados por posição");
            Console.WriteLine("3. Gerar times genérico (aleatório/customizado)");
            Console.WriteLine("4. Voltar");
            Console.Write("Escolha: ");
            var opcao = Console.ReadLine();

            if (opcao == "4") return;

            Console.Write("ID da partida para gerar times: ");
            if (!int.TryParse(Console.ReadLine(), out int idPartida))
            {
                Console.WriteLine("ID inválido.");
                continue;
            }

            var partida = partidaService.ObterPartidaPorId(idPartida);
            if (partida == null)
            {
                Console.WriteLine("Partida não encontrada.");
                continue;
            }

            List<Time> times = null;

            switch (opcao)
            {
                case "1":
                    times = timeService.GerarTimesOrdemChegada(partida.InteressadosRA, partida.JogadoresPorTime);
                    break;
                case "2":
                    times = timeService.GerarTimesEquilibrados(partida.InteressadosRA);
                    break;
                case "3":
                    times = timeService.GerarTimesGenerico(partida.InteressadosRA, partida.JogadoresPorTime);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    continue;
            }

            if (times == null || times.Count != 2)
            {
                Console.WriteLine("Não foi possível gerar os times.");
                continue;
            }

            for (int i = 0; i < times.Count; i++)
            {
                Console.WriteLine($"\n=== Time {i + 1} ({times[i].Nome}) ===");
                foreach (var ra in times[i].JogadoresRA)
                {
                    var jogador = jogadorService.ObterJogadorPorRA(ra);
                    if (jogador != null)
                        Console.WriteLine($"- {jogador.Nome} ({jogador.Posicao})");
                    else
                        Console.WriteLine($"- RA {ra} (não encontrado)");
                }
            }
        }
    }
}