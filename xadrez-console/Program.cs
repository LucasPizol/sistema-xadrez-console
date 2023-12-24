using tabuleiro;
using xadrez;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {

            try {
                PartidaXadrez partidaXadrez = new PartidaXadrez();


                Tela.ImprimirTabuleiro(partidaXadrez.Tab);

                Console.ReadLine();
            } catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

}