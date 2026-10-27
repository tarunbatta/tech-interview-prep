using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class King : Piece {
        public King () { }

        public King (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.King;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                return result;
            }
        }
    }
}