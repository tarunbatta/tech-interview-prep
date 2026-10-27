using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class Rook : Piece {
        public Rook () { }

        public Rook (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.Rook;
        }

        public override bool isValidMove (Board board, Position from, Position to) {
            bool result = false;

            if (base.isValidMove (board, from, to)) {
                if (from.x == to.x || from.y == to.y) {
                    result = true;
                }
            }

            return result;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                return result;
            }
        }
    }
}