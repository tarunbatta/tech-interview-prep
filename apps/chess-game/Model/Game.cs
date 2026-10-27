using System;
using System.Collections.Generic;

namespace ChessGame.Model {
    public class Game {
        public Board board { get; }
        public Player p1 { get; }
        public Player p2 { get; }
        public bool isGame { get; set; }
        public List<Move> moves { get; }

        public Game (Board board, Player p1, Player p2) {
            this.board = board;
            this.p1 = p1;
            this.p2 = p2;
            this.isGame = false;
        }

        public void PrintBoard () {
            Console.Clear ();

            for (int i = 0; i < this.board.size; i++) {
                for (int j = 0; j < this.board.size; j++) {
                    Console.Write ("[" + this.board.squares[i][j].color.ToString () + "] ");
                    Console.Write (this.board.squares[i][j].piece.type.ToString ());
                    Console.Write (" (" + this.board.squares[i][j].piece.color.ToString () + ")");
                }

                Console.WriteLine ("");
            }
        }

        public void StartGame () { }

        //public Helper.PieceType turn { get; set; }

        //public Square DoublePawnPush { get; set; } // Used for tracking valid en passant captures

        //public int Halfmoves { get; set; }

        //public bool CanWhiteCastleA { get; set; }

        //public bool CanWhiteCastleH { get; set; }

        //public bool CanBlackCastleA { get; set; }

        //public bool CanBlackCastleH { get; set; }
    }
}