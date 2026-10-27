using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class Knight : Piece {
        public Knight () { }

        public Knight (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.Knight;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                return result;
            }
        }
    }
}