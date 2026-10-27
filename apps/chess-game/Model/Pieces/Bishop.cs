using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class Bishop : Piece {
        public Bishop () { }

        public Bishop (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.Bishop;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                return result;
            }
        }
    }
}