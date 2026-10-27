using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class Queen : Piece {
        public Queen () { }

        public Queen (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.Queen;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                return result;
            }
        }
    }
}