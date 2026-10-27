using ChessGame.Model.Pieces;

namespace ChessGame.Model {
    public class Player {
        public string name { get; set; }

        public Helper.PieceColor color { get; set; }

        public Move move { get; set; }

        public Player (string name, Helper.PieceColor color, Board board) {
            this.name = name;
            this.color = color;

            if (this.color == Helper.PieceColor.White) {
                for (int i = 0; i < board.size; i++) {
                    board.squares[board.size - 2][i].piece = new Pawn (color = this.color, new Position (board.size - 2, i));
                }

                board.squares[board.size - 1][0].piece = new Rook (this.color, new Position (board.size - 1, 0));
                board.squares[board.size - 1][1].piece = new Bishop (this.color, new Position (board.size - 1, 1));
                board.squares[board.size - 1][2].piece = new Knight (this.color, new Position (board.size - 1, 2));
                board.squares[board.size - 1][3].piece = new Queen (this.color, new Position (board.size - 1, 3));
                board.squares[board.size - 1][4].piece = new King (this.color, new Position (board.size - 1, 4));
                board.squares[board.size - 1][5].piece = new Knight (this.color, new Position (board.size - 1, 5));
                board.squares[board.size - 1][6].piece = new Bishop (this.color, new Position (board.size - 1, 6));
                board.squares[board.size - 1][7].piece = new Rook (this.color, new Position (board.size - 1, 7));
            } else {
                board.squares[0][0].piece = new Rook (this.color, new Position (0, 0));
                board.squares[0][1].piece = new Bishop (this.color, new Position (0, 1));
                board.squares[0][2].piece = new Knight (this.color, new Position (0, 2));
                board.squares[0][3].piece = new Queen (this.color, new Position (0, 3));
                board.squares[0][4].piece = new King (this.color, new Position (0, 4));
                board.squares[0][5].piece = new Knight (this.color, new Position (0, 5));
                board.squares[0][6].piece = new Bishop (this.color, new Position (0, 6));
                board.squares[0][7].piece = new Rook (this.color, new Position (0, 7));

                for (int i = 0; i < board.size; i++) {
                    board.squares[1][i].piece = new Pawn (color = this.color, new Position (1, i));
                }
            }
        }
    }
}