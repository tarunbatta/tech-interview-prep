using ChessGame.Model.Pieces;

namespace ChessGame.Model {
    public class Move {
        public Player p { get; set; }

        public Square from { get; }

        public Square to { get; }

        public Piece pieceCaptured { get; }

        public Helper.PieceType promotion { get; }
    }
}