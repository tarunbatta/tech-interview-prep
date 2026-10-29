using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Castle on the Grid
    /// You are given a grid with both sides equal to N. Rows and columns are numbered from 0 to N-1.
    /// There is a castle on the intersection of the th row and the th column.
    /// Your task is to calculate the minimum number of steps it would take to move the castle from its initial position to the goal position(c,d).
    /// It is guaranteed that it is possible to reach the goal position from the initial position.
    /// Note: You can move the castle from cell (a,b) to any (x,y) in a single step if there is a straight line between (a,b) and (x,y)
    /// that does not contain any forbidden cell. Here, "X" denotes a forbidden cell.
    ///
    /// Input Format
    /// The first line contains an integer N, the size of the grid.
    /// The following  lines contains a string of length that consists of one of the following characters: "X" or ".".
    /// Here, "X" denotes a forbidden cell, and "." denotes an allowed cell.
    /// The last line contains a, b, denoting the initial position of the castle, and c, d, denoting the goal position.
    /// Here, a, b, c and d are space separated.
    ///
    /// Constraints
    /// 1 <= N <= 100
    /// 0 <= a,b,c,d < N
    ///
    /// Output Format
    /// Output a single line: The integer denoting the minimum number of steps required to move the castle to the goal position.
    ///
    /// Sample Input
    /// 3
    /// .   x   .
    /// .   x   .
    /// .   .   .
    /// 0 0 0 2
    ///
    /// Sample Output
    /// 3
    ///
    /// Explanation
    /// Here is a path that one could follow in order to reach the destination in  steps:
    /// (0,0) -> (2,0) -> (2,2) -> (0,2)
    /// </summary>
    public class CastleOnTheGrid {
        public static void Init (string[] args) {
            //args = new string[5];
            //args[0] = "3";
            //args[1] = ".X.";
            //args[2] = ".X.";
            //args[3] = "...";
            //args[4] = "0 0 0 2";

            //args = new string[6];
            //args[0] = "4";
            //args[1] = ".X..";
            //args[2] = "..X.";
            //args[3] = "X..X";
            //args[4] = ".X..";
            //args[5] = "3 3 0 0";

            args = new string[42];
            args[0] = "40";
            args[1] = "...X......XX.X...........XX....X.XX.....";
            args[2] = ".X..............X...XX..X...X........X.X";
            args[3] = "......X....X....X.........X...........X.";
            args[4] = ".X.X.X..X........X.....X.X...X.....X..X.";
            args[5] = "....X.X.X...X..........X..........X.....";
            args[6] = "..X......X....X....X...X....X.X.X....XX.";
            args[7] = "...X....X.......X..XXX.......X.X.....X..";
            args[8] = "...X.X.........X.X....X...X.........X...";
            args[9] = "XXXX..X......X.XX......X.X......XX.X..XX";
            args[10] = ".X........X....X.X......X..X....XX....X.";
            args[11] = "...X.X..X.X.....X...X....X..X....X......";
            args[12] = "....XX.X.....X.XX.X...X.X.....X.X.......";
            args[13] = ".X.X.X..............X.....XX..X.........";
            args[14] = "..X...............X......X........XX...X";
            args[15] = ".X......X...X.XXXX.....XX...........X..X";
            args[16] = "...X....XX....X...XX.X..X..X..X.....X..X";
            args[17] = "...X...X.X.....X.....X.....XXXX.........";
            args[18] = "X.....XX..X.............X.XX.X.XXX......";
            args[19] = ".....X.X..X..........X.X..X...X.X......X";
            args[20] = "...X.....X..X.............X......X..X..X";
            args[21] = "........X.....................X....X.X..";
            args[22] = "..........X.....XXX...XX.X..............";
            args[23] = "........X..X..........X.XXXX..X.........";
            args[24] = "..X..X...X.......XX...X.....X...XXX..X..";
            args[25] = ".X.......X..............X....X...X....X.";
            args[26] = ".................X.....X......X.....X...";
            args[27] = ".......X.X.XX..X.XXX.X.....X..........X.";
            args[28] = "X..X......X..............X..X.X.......X.";
            args[29] = "X........X.....X.....X....XX.......XX...";
            args[30] = "X.....X.................X.....X..X...XXX";
            args[31] = "XXX..X..X.X.XX..X....X.....XXX..X......X";
            args[32] = "..........X.....X.....XX................";
            args[33] = "..X.........X..X.........X...X.....X....";
            args[34] = ".X.X....X...XX....X...............X.....";
            args[35] = ".X....X....XX.XX........X..X............";
            args[36] = "X...X.X................XX......X..X.....";
            args[37] = "..X.X.......X.X..X.....XX.........X..X..";
            args[38] = "........................X..X.XX..X......";
            args[39] = "........X..X.X.....X.....X......X.......";
            args[40] = ".X..X....X.X......XX....................";
            args[41] = "34 28 16 8";

            int matrixLength = Convert.ToInt32 (args[0]);

            string[, ] matrix = new string[matrixLength, matrixLength];

            for (int i = 1; i <= matrixLength; i++) {
                char[] matrixInputs = args[i].ToCharArray ();

                for (int j = 0; j < matrixLength; j++) {
                    matrix[i - 1, j] = Convert.ToString (matrixInputs[j]);
                }
            }

            string[] coordinates = args[matrixLength + 1].Split (' ');
            int a = Convert.ToInt32 (coordinates[0]);
            int b = Convert.ToInt32 (coordinates[1]);
            int c = Convert.ToInt32 (coordinates[2]);
            int d = Convert.ToInt32 (coordinates[3]);

            Console.WriteLine (GetMinCastleToDestinationMoves (matrix, matrixLength, a, b, c, d));
        }

        public static int GetMinCastleToDestinationMoves (string[, ] matrix, int matrixLength, int a, int b, int c, int d) {
            if (matrix == null || (matrix != null && matrix.Length == 0)) {
                return 0;
            }

            int[, ] cost = new int[matrixLength, matrixLength];
            for (int i = 0; i < matrixLength; i++) {
                for (int j = 0; j < matrixLength; j++) {
                    cost[i, j] = Int32.MaxValue;
                }
            }

            Point start = new Point (a, b);

            Queue q = new Queue ();
            q.Enqueue (start);
            cost[a, b] = 0;

            while (q.Count > 0) {
                Point node = (Point) q.Dequeue ();

                if (node.row == c && node.col == d) {
                    break;
                }

                for (int i = node.row; i >= 0; i--) {
                    Point top = new Point (i, node.col);

                    if (matrix[top.row, top.col] == "X") {
                        break;
                    } else {
                        if (cost[top.row, top.col] == Int32.MaxValue) {
                            cost[top.row, top.col] = Math.Min (cost[top.row, top.col], cost[node.row, node.col] + 1);
                            q.Enqueue (top);
                        }
                    }
                }

                for (int i = node.col; i < matrixLength; i++) {
                    Point right = new Point (node.row, i);

                    if (matrix[right.row, right.col] == "X") {
                        break;
                    } else {
                        if (cost[right.row, right.col] == Int32.MaxValue) {
                            cost[right.row, right.col] = Math.Min (cost[right.row, right.col], cost[node.row, node.col] + 1);
                            q.Enqueue (right);
                        }
                    }
                }

                for (int i = node.row; i < matrixLength; i++) {
                    Point bottom = new Point (i, node.col);

                    if (matrix[bottom.row, bottom.col] == "X") {
                        break;
                    } else {
                        if (cost[bottom.row, bottom.col] == Int32.MaxValue) {
                            cost[bottom.row, bottom.col] = Math.Min (cost[bottom.row, bottom.col], cost[node.row, node.col] + 1);
                            q.Enqueue (bottom);
                        }
                    }
                }

                for (int i = node.col; i >= 0; i--) {
                    Point left = new Point (node.row, i);

                    if (matrix[left.row, left.col] == "X") {
                        break;
                    } else {
                        if (cost[left.row, left.col] == Int32.MaxValue) {
                            cost[left.row, left.col] = Math.Min (cost[left.row, left.col], cost[node.row, node.col] + 1);
                            q.Enqueue (left);
                        }
                    }
                }
            }

            return cost[c, d];
        }
    }

    public class Point {
        public int row { get; set; }
        public int col { get; set; }

        public Point (int row, int col) {
            this.row = row;
            this.col = col;
        }
    }
}