// Url:https://leetcode.com/problems/pascals-triangle

/*
118. Pascal's Triangle
Easy

Given a non-negative integer numRows, generate the first numRows of Pascal's triangle.


In Pascal's triangle, each number is the sum of the two numbers directly above it.

Example:

Input: 5
Output:
[
     [1],
    [1,1],
   [1,2,1],
  [1,3,3,1],
 [1,4,6,4,1]
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem118 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (Generate(0));
            Console.WriteLine (Generate(5));
        }

        public static IList<IList<int>> Generate (int numRows) {
            IList<IList<int>> result = [];

            for (int i = 1; i <= numRows; i++) {
                List<int> temp = [];

                for (int j = 1; j <= i; j++) {
                    if (j == 1 || j == i) {
                        temp.Add (1);
                    } else {
                        temp.Add (result[i - 2][j - 2] + result[i - 2][j - 1]);
                    }
                }

                result.Add (temp);
            }

            return result;
        }
    }
}