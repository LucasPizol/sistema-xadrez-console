using tabuleiro;
using xadrez;

namespace xadrez_console {
    internal class Tela {

        public static void ImprimirPartida(PartidaXadrez partida) {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
            if (partida.Xeque) {
                Console.WriteLine("VOCÊ ESTÁ EM XEQUE!");
            }
        }
        public static void ImprimirTabuleiro(Tabuleiro tab) {
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(tab.linhas - i + " ");
                for (int j = 0; j < tab.colunas; j++) {
                    ImprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida) {
            Console.WriteLine("Pecas capturadas: ");

            Console.Write("Cor Branca: ");
            foreach(Peca x in partida.PecasCapturadas(Cor.Branca)) {
                Console.Write($"{x} ");
            }
            Console.WriteLine();
            Console.Write("Cor Preta: ");
            foreach (Peca x in partida.PecasCapturadas(Cor.Preta)) {
                Console.Write($"{x} ");
            }
            Console.WriteLine();

        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;


            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(tab.linhas - i + " ");
                for (int j = 0; j < tab.colunas; j++) {
                    if (posicoesPossiveis[i,j]) {
                        Console.BackgroundColor = fundoAlterado;
                    } else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;

        }

        public static void ImprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("- ");
                Console.Write("");
                return;
            }

            if (peca.cor == Cor.Branca) {
                Console.Write(peca);
                Console.Write(" ");
                return;
            }

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(peca);
            Console.ForegroundColor = aux;

            Console.Write(" ");




        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}
