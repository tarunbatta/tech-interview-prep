/*
Amazon OA: Optimal Utilization

Given 2 lists a and b. Each element is a pair of integers where the first integer represents the unique id
and the second integer represents a value. Your task is to find an element from a and an element from b such
that the sum of their values is less or equal to target and as close to target as possible.
Return a list of ids of selected elements. If no pair is possible, return an empty list.

Example 1:
a = [[1, 2], [2, 4], [3, 6]]
b = [[1, 2]]
target = 7
Output: [[2, 1]]

Example 2:
a = [[1, 3], [2, 5], [3, 7], [4, 10]]
b = [[1, 2], [2, 3], [3, 4], [4, 5]]
target = 10
Output: [[2, 4], [3, 2]]

Example 3:
a = [[1, 8], [2, 7], [3, 14]]
b = [[1, 5], [2, 10], [3, 14]]
target = 20
Output: [[3, 1]]
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.Amazon {
    public class OptimalUtilization {
        public static void Init () {
            Print (Solve ([[1, 2], [2, 4], [3, 6]], [[1, 2]], 7)); // [2,1]
            Print (Solve ([[1, 3], [2, 5], [3, 7], [4, 10]], [[1, 2], [2, 3], [3, 4], [4, 5]], 10)); // [2,4] [3,2]
            Print (Solve ([[1, 8], [2, 7], [3, 14]], [[1, 5], [2, 10], [3, 14]], 20)); // [3,1]
            Print (Solve ([[1, 8], [2, 15], [3, 9]], [[1, 8], [2, 11], [3, 12]], 20)); // [1,3] [3,2]
        }

        // Sort both lists, then two pointers: i from start of a, j from end of b.
        // Time: O (n log n + m log m)
        // Space: O (n + m)
        public static List<int[]> Solve (int[][] a, int[][] b, int target) {
            int[][] sa = a.OrderBy (x => x[1]).ToArray ();
            int[][] sb = b.OrderBy (x => x[1]).ToArray ();

            List<int[]> result = new List<int[]> ();
            int best = int.MinValue;
            int i = 0, j = sb.Length - 1;

            while (i < sa.Length && j >= 0) {
                int sum = sa[i][1] + sb[j][1];

                if (sum > target) {
                    j--;
                    continue;
                }

                if (sum > best) {
                    best = sum;
                    result.Clear ();
                }

                if (sum == best) {
                    // Collect every b with the same value (duplicates).
                    int k = j;
                    while (k >= 0 && sb[k][1] == sb[j][1]) {
                        result.Add ([sa[i][0], sb[k][0]]);
                        k--;
                    }
                }

                i++;
            }

            return result;
        }

        private static void Print (List<int[]> pairs) {
            Console.WriteLine (string.Join (" ", pairs.Select (p => $"[{p[0]},{p[1]}]")));
        }
    }
}
