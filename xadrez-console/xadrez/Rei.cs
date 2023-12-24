using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) {

        }

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            if (p == null) {
                return true;
            }

            return p.cor != cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            for (int i = posicao.linha; i < posicao.linha + 3; i++) {
                for (int j = posicao.coluna; j < posicao.coluna + 3; j++) {
                    pos.DefinirValores(i - 1,j - 1);
                    if (tab.PosicaoValida(pos) && PodeMover(pos)) {
                        mat[pos.linha, pos.coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
