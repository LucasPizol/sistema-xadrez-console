
using tabuleiro;
namespace xadrez {
    internal class PartidaXadrez {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
        }
        public void ValidarPosicaoOrigem(Posicao pos) {
            if (Tab.peca(pos) == null) throw new TabuleiroException("Não existe peça nesta posição. ");
            if (JogadorAtual != Tab.peca(pos).cor) throw new TabuleiroException("A peça na origem escolhida não é sua.");
            if (!Tab.peca(pos).ExisteMovimentosPossiveis()) throw new TabuleiroException("Não há movimentos possíveis para esta peça.");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!Tab.peca(origem).PodeMoverPara(destino)) throw new TabuleiroException("Posição de destino inválida.");
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            Terminada = false;
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            ExecutaMovimento(origem, destino);
            MudaJogador();
            Turno++;
        }

        private void MudaJogador() {
            if (JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
                return;
            }

            JogadorAtual = Cor.Branca;
        }

        private void ColocarPecas() {
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('a', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('h', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('a', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('h', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('e', 4).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());


        }
    }
}
