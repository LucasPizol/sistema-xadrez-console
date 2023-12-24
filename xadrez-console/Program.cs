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

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    bool[,]  posicoesPossives = partidaXadrez.Tab.peca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaXadrez.Tab, posicoesPossives);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    Console.WriteLine();

                    partidaXadrez.ExecutaMovimento(origem, destino);
                } catch (TabuleiroException e) {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }

}