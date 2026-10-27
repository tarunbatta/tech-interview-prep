// Url: https://leetcode.com/problems/range-sum-query-2d-mutable

/*
308. Range Sum Query 2D - Mutable
Hard

Given a 2D matrix matrix, handle multiple queries of the following types:
1. Update the value of a cell in matrix.
2. Calculate the sum of the elements of matrix inside the rectangle defined by its upper left corner (row1, col1) and lower right corner (row2, col2).

Implement the NumMatrix class:
- NumMatrix(int[][] matrix) Initializes the object with the integer matrix.
- void update(int row, int col, int val) Updates the value of matrix[row][col] to be val.
- int sumRegion(int row1, int col1, int row2, int col2) Returns the sum of the elements of matrix inside the rectangle defined by its upper left corner (row1, col1) and lower right corner (row2, col2).

Example 1:
Input
["NumMatrix", "sumRegion", "update", "sumRegion"]
[[[[3, 0, 1, 4, 2], [5, 6, 3, 2, 1], [1, 2, 0, 1, 5], [4, 1, 0, 1, 7], [1, 0, 3, 0, 5]]], [2, 1, 4, 3], [3, 2, 2], [2, 1, 4, 3]]
Output
[null, 8, null, 10]

Explanation
NumMatrix numMatrix = new NumMatrix([[3, 0, 1, 4, 2], [5, 6, 3, 2, 1], [1, 2, 0, 1, 5], [4, 1, 0, 1, 7], [1, 0, 3, 0, 5]]);
numMatrix.sumRegion(2, 1, 4, 3); // return 8 (i.e. sum of the red rectangle)
numMatrix.update(3, 2, 2);       // matrix[3][2] is updated from 0 to 2
numMatrix.sumRegion(2, 1, 4, 3); // return 10 (i.e. sum of the red rectangle)

Constraints:
m == matrix.length
n == matrix[i].length
1 <= m, n <= 200
-10^5 <= matrix[i][j] <= 10^5
0 <= row < m
0 <= col < n
-10^5 <= val <= 10^5
0 <= row1 <= row2 < m
0 <= col1 <= col2 < n
At most 10^4 calls will be made to update and sumRegion.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem308
{
    public class Solution
    {
        public void Init()
        {
            int[][] matrix = new int[][]
            {
                new int[] { 3, 0, 1, 4, 2 },
                new int[] { 5, 6, 3, 2, 1 },
                new int[] { 1, 2, 0, 1, 5 },
                new int[] { 4, 1, 0, 1, 7 },
                new int[] { 1, 0, 3, 0, 5 }
            };

            NumMatrix numMatrix = new(matrix);
            Console.WriteLine(numMatrix.SumRegion(2, 1, 4, 3)); // 8
            numMatrix.Update(3, 2, 2);
            Console.WriteLine(numMatrix.SumRegion(2, 1, 4, 3)); // 10
        }
    }

    // Time:
    //   Constructor: O(m * n * log m * log n)
    //   Update: O(log m * log n)
    //   SumRegion: O(log m * log n)
    // Space: O(m * n) for the 2D Binary Indexed Tree
    public class NumMatrix
    {
        private readonly int[][] _nums;
        private readonly int[][] _tree;
        private readonly int _m;
        private readonly int _n;

        public NumMatrix(int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            {
                return;
            }

            _m = matrix.Length;
            _n = matrix[0].Length;

            _nums = new int[_m][];
            for (int i = 0; i < _m; i++)
            {
                _nums[i] = new int[_n];
            }

            _tree = new int[_m + 1][];
            for (int i = 0; i <= _m; i++)
            {
                _tree[i] = new int[_n + 1];
            }

            for (int i = 0; i < _m; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    Update(i, j, matrix[i][j]);
                }
            }
        }

        public void Update(int row, int col, int val)
        {
            if (_m == 0 || _n == 0)
            {
                return;
            }

            int delta = val - _nums[row][col];
            _nums[row][col] = val;

            for (int i = row + 1; i <= _m; i += i & -i)
            {
                for (int j = col + 1; j <= _n; j += j & -j)
                {
                    _tree[i][j] += delta;
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            if (_m == 0 || _n == 0)
            {
                return 0;
            }

            return Query(row2, col2) - Query(row1 - 1, col2) - Query(row2, col1 - 1) + Query(row1 - 1, col1 - 1);
        }

        private int Query(int row, int col)
        {
            int sum = 0;

            for (int i = row + 1; i > 0; i -= i & -i)
            {
                for (int j = col + 1; j > 0; j -= j & -j)
                {
                    sum += _tree[i][j];
                }
            }

            return sum;
        }
    }
}