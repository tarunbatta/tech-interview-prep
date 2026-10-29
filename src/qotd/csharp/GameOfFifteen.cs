using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Gem Puzzle | Boss Puzzle | Game of Fifteen | Mystic Square
    /// The 15 puzzle is a classic puzzle made famous in the 19th century.
    /// https://en.wikipedia.org/wiki/15_puzzle
    /// It consists of 4x4 board with 15 sliding tiles numbered from 1 to 15.
    /// The objective is to get them into this pattern:
    ///     1    2    3    4
    ///     5    6    7    8
    ///     9    10   11   12
    ///     13   14   15
    ///
    /// Write a program, that given some initial state of the 4x4 board, finds a sequence of moves that transforms it so
    /// back to the initial pattern (the hole should always be in the lower right corner). The less moves you use, the better.
    ///
    /// Input
    /// A 4x4 integer array that represents the position of all 15 tiles.
    /// The position of the hole is indicated by 0.
    ///
    /// Output
    /// The sequence of moves that solves the puzzle. That is, all the moves that happen with respect to the hole.
    /// Write 'D' to move the hole down, 'U' to move it up, 'R' to move it right and 'L' to move it left.
    /// All moves should be valid (so for example don't try to move the hole up when it is in the first row).
    /// </summary>
    public class GameOfFifteen {
        public static void Init (string[] args) {
            FifteenPuzzle (new int[, ] { { 5, 1, 2, 3 }, { 4, 0, 6, 7 }, { 8, 9, 10, 11 }, { 12, 13, 14, 15 }
            });
        }

        public static void FifteenPuzzle (int[, ] pieces) {
            GameOfFifteenBoard board = new GameOfFifteenBoard (pieces);

            while (!board.IsComplete ()) {
                char move = board.GetBestMove ();

                switch (move) {
                    case 'U':
                        board.MoveUp ();
                        break;

                    case 'R':
                        board.MoveRight ();
                        break;

                    case 'B':
                        board.MoveBottom ();
                        break;

                    case 'L':
                        board.MoveLeft ();
                        break;
                }
            }

            Console.WriteLine (string.Join (",", board.moves));
        }
    }

    public class GameOfFifteenPosition {
        public int row { get; set; }
        public int col { get; set; }

        public GameOfFifteenPosition (int row, int col) {
            this.row = row;
            this.col = col;
        }
    }

    public class GameOfFifteenBoard {
        public GameOfFifteenPosition current { get; set; }
        public int size { get; set; }
        public int[, ] pieces { get; set; }
        public IList<char> moves { get; set; }

        public GameOfFifteenBoard (int[, ] pieces) {
            this.pieces = pieces;
            this.moves = new List<char> ();

            if (this.pieces != null) {
                int rows = this.pieces.GetLength (0);
                int cols = this.pieces.GetLength (1);

                if (rows == cols && rows > 0) {
                    this.size = rows;
                    this.current = GetCurrent ();
                }
            }
        }

        public void MoveUp () {
            if (this.current.row != 0) {
                this.pieces[this.current.row, this.current.col] = this.pieces[this.current.row - 1, this.current.col];
                this.pieces[this.current.row - 1, this.current.col] = 0;
                this.current.row--;

                this.moves.Add ('U');
            }
        }

        public void MoveRight () {
            if (this.current.col != this.size - 1) {
                this.pieces[this.current.row, this.current.col] = this.pieces[this.current.row, this.current.col + 1];
                this.pieces[this.current.row, this.current.col + 1] = 0;
                this.current.col++;

                this.moves.Add ('R');
            }
        }

        public void MoveBottom () {
            if (this.current.row != this.size - 1) {
                this.pieces[this.current.row, this.current.col] = this.pieces[this.current.row + 1, this.current.col];
                this.pieces[this.current.row + 1, this.current.col] = 0;
                this.current.row++;

                this.moves.Add ('B');
            }
        }

        public void MoveLeft () {
            if (this.current.col != 0) {
                this.pieces[this.current.row, this.current.col] = this.pieces[this.current.row, this.current.col - 1];
                this.pieces[this.current.row, this.current.col - 1] = 0;
                this.current.col--;

                this.moves.Add ('L');
            }
        }

        public GameOfFifteenPosition GetCurrent () {
            GameOfFifteenPosition result;

            if (this.pieces != null) {
                int rows = this.pieces.GetLength (0);
                int cols = this.pieces.GetLength (1);

                if (rows == cols && rows > 0) {
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < cols; j++) {
                            if (this.pieces[i, j] == 0) {
                                result = new GameOfFifteenPosition (i, j);
                                return result;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public char GetBestMove () {
            char result = new char ();

            Dictionary<char, int> bestMoves = new Dictionary<char, int> ();
            bestMoves.Add ('U', GetSortFactor ('U'));
            bestMoves.Add ('R', GetSortFactor ('R'));
            bestMoves.Add ('B', GetSortFactor ('B'));
            bestMoves.Add ('L', GetSortFactor ('L'));

            var bestMove = bestMoves.ToList ();
            bestMove.Sort ((x, y) => x.Value.CompareTo (y.Value));
            result = bestMove[0].Key;

            return result;
        }

        private int GetSortFactor (char move) {
            int result = Int32.MaxValue;

            switch (move) {
                case 'U':

                    break;

                case 'R':

                    break;

                case 'B':

                    break;

                case 'L':

                    break;
            }

            return result;
        }

        public bool IsComplete () {
            bool result = false;

            if (this.pieces[this.size - 1, this.size - 1] == 0) {
                for (int i = 0; i < this.size - 1; i++) {
                    for (int j = 0; j < this.size - 1; j++) {
                        if (this.pieces[i, j] != ((i + 1) + this.size * j)) {
                            return false;
                        }
                    }
                }
            }

            return result;
        }
    }
}