/*
Amazon OA: Treasure Island (I and II)

Treasure Island I:
You have a map that marks the location of a treasure island. Some of the map area has jagged rocks and dangerous
reefs. Other areas are safe to sail in. Find the minimum route (number of steps) from the top-left corner to the
treasure. You can move one block up, down, left or right at a time.
'O' = safe, 'D' = dangerous, 'X' = treasure. The top-left corner is always safe.

Input:
[['O', 'O', 'O', 'O'],
 ['D', 'O', 'D', 'O'],
 ['O', 'O', 'O', 'O'],
 ['X', 'D', 'D', 'O']]
Output: 5   Route: (0,0) -> (0,1) -> (1,1) -> (2,1) -> (2,0) -> (3,0)

Treasure Island II (multi-source BFS):
Same map, but there are multiple starting points 'S' and multiple treasure islands 'X'. Find the minimum route
from any starting point to any treasure island.

Input:
[['S', 'O', 'O', 'S', 'S'],
 ['D', 'O', 'D', 'O', 'D'],
 ['O', 'O', 'O', 'O', 'X'],
 ['X', 'D', 'D', 'O', 'O'],
 ['X', 'D', 'D', 'D', 'O']]
Output: 3
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Amazon {
    public class TreasureIsland {
        private static readonly int[][] Dirs = [[1, 0], [-1, 0], [0, 1], [0, -1]];

        public static void Init () {
            char[][] map1 = [
                ['O', 'O', 'O', 'O'],
                ['D', 'O', 'D', 'O'],
                ['O', 'O', 'O', 'O'],
                ['X', 'D', 'D', 'O'],
            ];
            Console.WriteLine (MinSteps (map1)); // 5

            char[][] map2 = [
                ['S', 'O', 'O', 'S', 'S'],
                ['D', 'O', 'D', 'O', 'D'],
                ['O', 'O', 'O', 'O', 'X'],
                ['X', 'D', 'D', 'O', 'O'],
                ['X', 'D', 'D', 'D', 'O'],
            ];
            Console.WriteLine (MinStepsMultiSource (map2)); // 3
        }

        // Single-source BFS from (0,0).
        // Time: O (R * C)
        // Space: O (R * C)
        public static int MinSteps (char[][] grid) {
            return Bfs (grid, [(0, 0)]);
        }

        // Multi-source BFS: seed the queue with every 'S' at distance 0.
        // Time: O (R * C)
        // Space: O (R * C)
        public static int MinStepsMultiSource (char[][] grid) {
            List<(int, int)> sources = new List<(int, int)> ();

            for (int r = 0; r < grid.Length; r++) {
                for (int c = 0; c < grid[r].Length; c++) {
                    if (grid[r][c] == 'S') {
                        sources.Add ((r, c));
                    }
                }
            }

            return Bfs (grid, sources);
        }

        private static int Bfs (char[][] grid, List<(int r, int c)> sources) {
            if (grid == null || grid.Length == 0) {
                return -1;
            }

            int rows = grid.Length, cols = grid[0].Length;
            bool[, ] visited = new bool[rows, cols];
            Queue<(int r, int c)> queue = new Queue<(int, int)> ();

            foreach ((int r, int c) in sources) {
                queue.Enqueue ((r, c));
                visited[r, c] = true;
            }

            int steps = 0;

            while (queue.Count > 0) {
                for (int size = queue.Count; size > 0; size--) {
                    (int r, int c) = queue.Dequeue ();

                    if (grid[r][c] == 'X') {
                        return steps;
                    }

                    foreach (int[] d in Dirs) {
                        int nr = r + d[0], nc = c + d[1];

                        if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && !visited[nr, nc] && grid[nr][nc] != 'D') {
                            visited[nr, nc] = true;
                            queue.Enqueue ((nr, nc));
                        }
                    }
                }

                steps++;
            }

            return -1;
        }
    }
}
