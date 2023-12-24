
using tabuleiro;
using System.Collections.Generic;

namespace xadrez {
    internal class PartidaXadrez {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
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

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            Terminada = false;

            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapt) {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();

            if (pecaCapt != null) {
                Tab.ColocarPeca(pecaCapt, destino);
                capturadas.Remove(pecaCapt);
            }
            Tab.ColocarPeca(p, origem);

        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque.");
            }

            if (EstaEmXeque(CorAdversaria(JogadorAtual))) {
                Xeque = true;
            } else {
                Xeque = false;
            }

            if (TesteXequemate(CorAdversaria(JogadorAtual))) {
                Terminada = true;
                return;
            }

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

        public HashSet<Peca> PecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);

                }

            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);

                }

            }

            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca) {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);

        }

        private void ColocarPecas() {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preta));
        }

        private Cor CorAdversaria(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            }

            return Cor.Branca;
        }

        private Peca ReiPorCor(Cor cor) {
            foreach (Peca x in PecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca R = ReiPorCor(cor);

            if (R == null) throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro.");

            foreach (Peca x in PecasEmJogo(CorAdversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequemate(Cor cor) {
            if(!EstaEmXeque(cor)) return false;

            foreach(Peca x in PecasEmJogo(cor)) {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.linhas; i ++) {
                    for (int j = 0; j < Tab.colunas; j ++) {
                        if (mat[i,j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
