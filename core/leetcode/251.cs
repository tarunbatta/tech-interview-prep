// Url: https://leetcode.com/problems/flatten-2d-vector

/*
251. Flatten 2D Vector
Medium

Design an iterator to flatten a 2D vector. It should support next and hasNext operations.

Implement the Vector2D class:

- Vector2D(int[][] vec) initializes the object with the 2D vector vec.
- next() returns the next element from the 2D vector and moves the pointer one step forward.
- hasNext() returns true if there are still elements in the vector, and false otherwise.

Example 1:
Input
["Vector2D", "next", "next", "next", "hasNext", "hasNext", "next", "hasNext"]
[[[[1, 2], [3], [4]]], [], [], [], [], [], [], []]
Output
[null, 1, 2, 3, true, true, 4, false]

Explanation
Vector2D vector2D = new Vector2D([[1, 2], [3], [4]]);
vector2D.next();    // return 1
vector2D.next();    // return 2
vector2D.next();    // return 3
vector2D.hasNext(); // return True
vector2D.hasNext(); // return True
vector2D.next();    // return 4
vector2D.hasNext(); // return False

Constraints:
0 <= vec.length <= 1000
0 <= vec[i].length <= 500
-1000 <= vec[i][j] <= 1000
At most 10^5 calls will be made to next and hasNext.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem251
{
    public class Solution
    {
        public void Init()
        {
            int[][] vec = new int[][]
            {
                new int[] { 1, 2 },
                new int[] { 3 },
                new int[] { 4 }
            };

            Vector2D vector2D = new(vec);
            Console.WriteLine(vector2D.Next());    // 1
            Console.WriteLine(vector2D.Next());    // 2
            Console.WriteLine(vector2D.Next());    // 3
            Console.WriteLine(vector2D.HasNext()); // True
            Console.WriteLine(vector2D.Next());    // 4
            Console.WriteLine(vector2D.HasNext()); // False
        }
    }

    // Time:
    //   Next: O(1) amortized
    //   HasNext: O(1) amortized
    // Space: O(1) auxiliary space (operates directly on input references)
    public class Vector2D
    {
        private readonly int[][] _vec;
        private int _row;
        private int _col;

        public Vector2D(int[][] vec)
        {
            _vec = vec ?? Array.Empty<int[]>();
            _row = 0;
            _col = 0;
        }

        public int Next()
        {
            Advance();
            return _vec[_row][_col++];
        }

        public bool HasNext()
        {
            Advance();
            return _row < _vec.Length;
        }

        private void Advance()
        {
            while (_row < _vec.Length && (_vec[_row] == null || _col >= _vec[_row].Length))
            {
                _row++;
                _col = 0;
            }
        }
    }
}