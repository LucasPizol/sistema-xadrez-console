using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {

        private PartidaXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaXadrez partida) : base(tab, cor) {
            this.partida = partida;
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

        private bool TesteTorreParaRoque(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && qtdMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            for (int i = posicao.linha; i < posicao.linha + 3; i++) {
                for (int j = posicao.coluna; j < posicao.coluna + 3; j++) {
                    pos.DefinirValores(i - 1, j - 1);
                    if (tab.PosicaoValida(pos) && PodeMover(pos)) {
                        mat[pos.linha, pos.coluna] = true;
                    }
                }
            }

            if (qtdMovimentos == 0 && !partida.Xeque) {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);

                if(TesteTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }

                }


            }

            if (qtdMovimentos == 0 && !partida.Xeque) {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna - 4);

                if (TesteTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna -2] = true;
                    }

                }


            }


            return mat;
        }

    }
}
