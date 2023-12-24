using tabuleiro;
using xadrez;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            PartidaXadrez partidaXadrez = new PartidaXadrez();


            while (!partidaXadrez.Terminada) {
                try {

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaXadrez.Tab);
                    Console.WriteLine($"Turno: {partidaXadrez.Turno}");
                    Console.WriteLine($"Aguardando a jogada: {partidaXadrez.JogadorAtual}");
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaXadrez.ValidarPosicaoOrigem(origem);

                    bool[,]  posicoesPossives = partidaXadrez.Tab.peca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaXadrez.Tab, posicoesPossives);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaXadrez.ValidarPosicaoDestino(origem, destino);

                    Console.WriteLine();

                    partidaXadrez.RealizaJogada(origem, destino);
                } catch (TabuleiroException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();

                } catch {
                    Console.WriteLine("Posição inválida");
                    Console.ReadLine();
                }
            }

        }
    }

}