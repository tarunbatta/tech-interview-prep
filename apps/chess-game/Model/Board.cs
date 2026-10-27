namespace ChessGame.Model {
    public class Board {
        public int size { get; set; }

        public Square[][] squares { get; set; }

        public Board (int size) {
            this.size = size;
            this.squares = new Square[size][];

            for (int i = 0; i < this.size; i++) {
                this.squares[i] = new Square[size];
                for (int j = 0; j < this.size; j++) {
                    if ((i + j) % 2 == 0) {
                        this.squares[i][j] = new Square (Helper.PieceColor.Black, new Position (i, j));
                    } else {
                        this.squares[i][j] = new Square (Helper.PieceColor.White, new Position (i, j));
                    }
                }
            }
        }
    }
}