using ChessGame.Model.Pieces;

namespace ChessGame.Model {
    public class Square {
        public Helper.PieceColor color { get; set; }
        public Piece piece { get; set; }
        public Position position { get; set; }

        public Square (Helper.PieceColor color, Position position) {
            this.color = color;
            this.position = position;
            this.piece = null;
        }

        public Square (Helper.PieceColor color, Position position, Piece piece) {
            this.color = color;
            this.position = position;
            this.piece = piece;
        }

        public bool IsOccupied {
            get {
                return (this.piece != null);
            }
        }

        public Piece ReleaseSpot () {
            Piece remove = this.piece;
            this.piece = null;
            return remove;
        }
    }
}