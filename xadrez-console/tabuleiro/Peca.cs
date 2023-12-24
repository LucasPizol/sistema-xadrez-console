namespace tabuleiro {
    abstract class Peca {

        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor) {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }

        public void IncrementarQtdMovimentos() {
            qtdMovimentos++;
        }
        public void DecrementarQtdMovimentos() {
            qtdMovimentos--;
        }

        public bool ExisteMovimentosPossiveis() {
            bool[,] mat = MovimentosPossiveis();

            for (int i =0; i < tab.linhas; i ++) {
                for (int j = 0; j < tab.colunas; j ++) {
                    if (mat[i,j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao pos) {
            return MovimentosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] MovimentosPossiveis();

        
    }
}
