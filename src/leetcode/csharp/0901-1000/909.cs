// Url:https://leetcode.com/problems/snakes-and-ladders

/*
909. Snakes and Ladders
Medium

On an N x N board, the numbers from 1 to N*N are written boustrophedonically starting from the bottom left of the board, and alternating direction each row.  For example, for a 6 x 6 board, the numbers are written as follows:




You start on square 1 of the board (which is always in the last row and first column).  Each move, starting from square x, consists of the following:


	You choose a destination square S with number x+1, x+2, x+3, x+4, x+5, or x+6, provided this number is <= N*N.

	
		(This choice simulates the result of a standard 6-sided die roll: ie., there are always at most 6 destinations, regardless of the size of the board.)
	
	
	If S has a snake or ladder, you move to the destination of that snake or ladder.  Otherwise, you move to S.


A board square on row r and column c has a "snake or ladder" if board[r][c] != -1.  The destination of that snake or ladder is board[r][c].

Note that you only take a snake or ladder at most once per move: if the destination to a snake or ladder is the start of another snake or ladder, you do not continue moving.  (For example, if the board is `[[4,-1],[-1,3]]`, and on the first move your destination square is `2`, then you finish your first move at `3`, because you do not continue moving to `4`.)

Return the least number of moves required to reach square N*N.  If it is not possible, return -1.

Example 1:

Input: [
[-1,-1,-1,-1,-1,-1],
[-1,-1,-1,-1,-1,-1],
[-1,-1,-1,-1,-1,-1],
[-1,35,-1,-1,13,-1],
[-1,-1,-1,-1,-1,-1],
[-1,15,-1,-1,-1,-1]]
Output: 4
Explanation: 
At the beginning, you start at square 1 [at row 5, column 0].
You decide to move to square 2, and must take the ladder to square 15.
You then decide to move to square 17 (row 3, column 5), and must take the snake to square 13.
You then decide to move to square 14, and must take the ladder to square 35.
You then decide to move to square 36, ending the game.
It can be shown that you need at least 4 moves to reach the N*N-th square, so the answer is 4.


Note:


	2 <= board.length = board[0].length <= 20
	board[i][j] is between 1 and N*N or is equal to -1.
	The board square with number 1 has no snake or ladder.
	The board square with number N*N has no snake or ladder.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem909 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n ^ 2)
        // Space: O (1)
        public static int SnakesAndLadders (int[][] board) {
            if (board == null) { return 0; }

            var moves = -1;
            var n = board.Length;
            var visited = new bool[(n * n) + 1];

            var q = new Queue<int> ();
            q.Enqueue (1);

            while (q.Count > 0) {
                var size = q.Count;
                moves++;

                for (var i = 0; i < size; i++) {
                    var c = q.Dequeue ();

                    visited[c] = true;

                    if (c == n * n) { return moves; }

                    for (var step = 1; step <= 6; step++) {
                        var cell = c + step;

                        var bottomRow = (int) Math.Ceiling ((double) cell / n) - 1;

                        var rowIdx = ((n - 1) - bottomRow);
                        var colIdx = bottomRow % 2 == 0 ? ((cell - 1) % n) : ((n - 1) - ((cell - 1) % n));

                        if (rowIdx < 0 || colIdx < 0 || rowIdx >= n || colIdx >= n) { continue; }

                        var nextCell = board[rowIdx][colIdx] == -1 ? cell : board[rowIdx][colIdx];

                        if (visited[nextCell] == false) {
                            q.Enqueue (nextCell);
                        }
                    }
                }
            }

            return -1;
        }
    }
}