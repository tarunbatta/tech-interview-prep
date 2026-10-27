using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public abstract class Piece {
        public Helper.PieceColor color { get; set; }
        public Position position { get; set; }
        public virtual List<Move> possibleMoves { get; set; }
        public Helper.PieceType type { get; set; }

        public bool isAlive {
            get {
                bool result = false;

                if (this.position != null && this.position.x > 0 && this.position.y > 0) {
                    result = true;
                }

                return result;
            }
        }

        public virtual bool isValidMove (Board board, Position from, Position to) {
            bool result = false;

            if (board != null && from != null && to != null) {
                if (from == to) {
                    result = false;
                } else if (from.x < 0 || from.y < 0 || from.x > board.size - 1 || from.y > board.size ||
                    to.x < 0 || to.y < 0 || to.x > board.size - 1 || to.y > board.size) {
                    result = false;
                } else {
                    result = true;
                }
            }

            return result;
        }
    }
}