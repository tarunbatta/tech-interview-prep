using System.Collections.Generic;

namespace ChessGame.Model.Pieces {
    public class Pawn : Piece {
        public Pawn () { }

        public Pawn (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.type = Helper.PieceType.Pawn;
        }

        public override List<Move> possibleMoves {
            get {
                List<Move> result = new List<Move> ();

                if (this.color == Helper.PieceColor.White) { } else { }

                return result;
            }
        }
    }
}