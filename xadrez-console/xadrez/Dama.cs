using tabuleiro;

namespace xadrez {
    internal class Dama : Peca {
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor) {

        }

        public override string ToString() {
            return "D";
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

            //NO
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.DefinirValores(pos.linha - 1, pos.coluna - 1);
            }

            //NE
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.DefinirValores(pos.linha - 1, pos.coluna + 1);
            }

            //SE
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.DefinirValores(pos.linha + 1, pos.coluna + 1);
            }

            //SO
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.DefinirValores(pos.linha + 1, pos.coluna - 1);
            }

            //acima
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.linha -= 1;
            }

            //abaixo
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.linha += 1;
            }

            //direita
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.coluna += 1;
            }

            //esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                pos.coluna -= 1;
            }

            return mat;

        }
    }
}
