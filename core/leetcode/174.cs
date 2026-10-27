// Url:https://leetcode.com/problems/dungeon-game

/*
174. Dungeon Game
Hard

table.dungeon, .dungeon th, .dungeon td {
  border:3px solid black;
}

 .dungeon th, .dungeon td {
    text-align: center;
    height: 70px;
    width: 70px;
}

The demons had captured the princess (P) and imprisoned her in the bottom-right corner of a dungeon. The dungeon consists of M x N rooms laid out in a 2D grid. Our valiant knight (K) was initially positioned in the top-left room and must fight his way through the dungeon to rescue the princess.

The knight has an initial health point represented by a positive integer. If at any point his health point drops to 0 or below, he dies immediately.

Some of the rooms are guarded by demons, so the knight loses health (negative integers) upon entering these rooms; other rooms are either empty (0's) or contain magic orbs that increase the knight's health (positive integers).

In order to reach the princess as quickly as possible, the knight decides to move only rightward or downward in each step.

 

Write a function to determine the knight's minimum initial health so that he is able to rescue the princess.

For example, given the dungeon below, the initial health of the knight must be at least 7 if he follows the optimal path RIGHT-> RIGHT -> DOWN -> DOWN.


	
		
			-2 (K)
			-3
			3
		
		
			-5
			-10
			1
		
		
			10
			30
			-5 (P)
		
	


 

Note:


	The knight's health has no upper bound.
	Any room can contain threats or power-ups, even the first room the knight enters and the bottom-right room where the princess is imprisoned.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem174 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (CalculateMinimumHP([
                [-2, -3, 3],
                    [-5, -10, 1],
                    [10, 30, -5]
            ]));
            Console.WriteLine (CalculateMinimumHP([
                [-2, -3, 3, 1],
                    [-5, -10, 1, -1],
                    [10, 30, -5, -1]
            ]));
        }

        public static int CalculateMinimumHP (int[][] dungeon) {
            int height = dungeon.GetLength (0);
            int width = dungeon.GetLength (1);

            int rowCounter = 0;
            int colCounter = 0;

            List<Knight> reachedPrincess = [];

            Knight k = new Knight (rowCounter, colCounter, dungeon[rowCounter][colCounter], null);
            Knight first = null;

            Queue q = new Queue ();
            q.Enqueue (k);

            while (q.Count > 0) {
                first = (Knight) q.Dequeue ();
                rowCounter = first.position.row;
                colCounter = first.position.col;

                if ((rowCounter == height - 1) && (colCounter == width - 1)) {
                    reachedPrincess.Add (first);
                }

                if ((rowCounter < height) && (colCounter + 1 < width)) {
                    Knight moveRight = new Knight (first.position.row, first.position.col, first.affect, first.moves);
                    moveRight.MoveRight (dungeon[rowCounter][colCounter + 1]);
                    q.Enqueue (moveRight);
                }

                if ((rowCounter + 1 < height) && (colCounter < width)) {
                    Knight moveDown = new Knight (first.position.row, first.position.col, first.affect, first.moves);
                    moveDown.MoveDown (dungeon[rowCounter + 1][colCounter]);
                    q.Enqueue (moveDown);
                }
            }

            var bestKnight = reachedPrincess.Where (x => x.health > 0)
                .OrderBy (x => x.health)
                .First ();

            return bestKnight.health;
        }
    }

    public class Position {
        public int row { get; set; }
        public int col { get; set; }

        public Position (int row, int col) {
            this.row = row;
            this.col = col;
        }
    }

    public class Knight {
        public int affect { get; set; }

        public List<string> moves = null;

        public Position position { get; set; }

        public int health {
            get {
                return -(this.affect) + 1;
            }
        }

        public Knight (int row, int col, int affect, List<string> moves) {
            this.affect = affect;
            this.position = new Position (row, col);

            if (moves == null || moves.Count == 0) {
                this.moves = ["START"];
            } else {
                this.moves = [.. moves];
            }
        }

        public void MoveRight (int affect) {
            this.position.col++;
            this.affect += affect;
            this.moves.Add ("RIGHT");
        }

        public void MoveDown (int affect) {
            this.position.row++;
            this.affect += affect;
            this.moves.Add ("DOWN");
        }
    }
}