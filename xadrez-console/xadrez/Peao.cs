using tabuleiro;

namespace xadrez {
    internal class Peao : Peca {

        private PartidaXadrez _partidaXadrez;
        public Peao(Tabuleiro tab, Cor cor, PartidaXadrez partidaXadrez) : base(tab, cor) {
            _partidaXadrez = partidaXadrez; 
        }

        public override string ToString() {
            return "P";
        }

        private bool PodeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            if (p == null) {
                return true;
            }

            return p.cor != cor;
        }

        private bool ExisteInimigo(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;   
        }
        private bool Livre(Posicao pos) {
            return tab.peca(pos) == null;
        }
        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca) {
                pos.DefinirValores(posicao.linha - 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha - 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos) && qtdMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;

                }

                pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //En Passant;
                if (posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == _partidaXadrez.VulneravelEnPassant) {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == _partidaXadrez.VulneravelEnPassant) {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            } else {
                pos.DefinirValores(posicao.linha + 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha + 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos) && qtdMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;

                }

                pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                //En Passant;
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == _partidaXadrez.VulneravelEnPassant) {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == _partidaXadrez.VulneravelEnPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }



            }

           


            return mat;
        }
    }
}
