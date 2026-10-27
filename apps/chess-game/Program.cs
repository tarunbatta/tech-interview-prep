using System;
using ChessGame.Model;

namespace ChessGame {
    internal class Program {
        private static void Main(string[] args)
        {
            Board board = new Board(8);
            Player p1 = new Player("Computer", Helper.PieceColor.Black, board);
            Player p2 = new Player("Tarun", Helper.PieceColor.White, board);

            Game game = new Game(board, p1, p2);
            Console.WriteLine("Chess Game initialized successfully.");
        }
    }
}