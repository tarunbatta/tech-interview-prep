// Url:https://leetcode.com/problems/unique-binary-search-trees

/*
96. Unique Binary Search Trees
Medium

Given n, how many structurally unique BST's (binary search trees) that store values 1 ... n?

Example:

Input: 3
Output: 5
Explanation:
Given n = 3, there are a total of 5 unique BST's:

   1         3     3      2      1
    \       /     /      / \      \
     3     2     1      1   3      2
    /     /       \                 \
   2     1         2                 3
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem96 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (NumTrees_a(0));
            Console.WriteLine (NumTrees_a(1));
            Console.WriteLine (NumTrees_a(2));
            Console.WriteLine (NumTrees_a(3));
            Console.WriteLine (NumTrees_a(4));
            Console.WriteLine (NumTrees_a(5));

            Console.WriteLine (NumTrees_b(0));
            Console.WriteLine (NumTrees_b(1));
            Console.WriteLine (NumTrees_b(2));
            Console.WriteLine (NumTrees_b(3));
            Console.WriteLine (NumTrees_b(4));
            Console.WriteLine (NumTrees_b(5));
        }

        public static int NumTrees_a (int n) {
            if (n < 0) {
                return 0;
            }

            if (n < 2) {
                return 1;
            }

            int[] g = new int[n + 1];
            g[0] = 1;
            g[1] = 1;

            for (int i = 2; i <= n; i++) {
                for (int j = 1; j <= i; j++) {
                    g[i] += g[j - 1] * g[i - j];
                }
            }

            return g[n];
        }

        // Catalan Number
        public static int NumTrees_b (int n) {
            long c = 1;

            for (int i = 0; i < n; ++i) {
                c = c * 2 * (2 * i + 1) / (i + 2);
            }

            return (int) c;
        }
    }
}