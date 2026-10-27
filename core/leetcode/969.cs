// Url:https://leetcode.com/problems/pancake-sorting

/*
969. Pancake Sorting
Medium

Given an array A, we can perform a pancake flip: We choose some positive integer k <= A.length, then reverse the order of the first k elements of A.  We want to perform zero or more pancake flips (doing them one after another in succession) to sort the array A.

Return the k-values corresponding to a sequence of pancake flips that sort A.  Any valid answer that sorts the array within 10 * A.length flips will be judged as correct.

 

Example 1:

Input: [3,2,4,1]
Output: [4,2,4,3]
Explanation: 
We perform 4 pancake flips, with k values 4, 2, 4, and 3.
Starting state: A = [3, 2, 4, 1]
After 1st flip (k=4): A = [1, 4, 2, 3]
After 2nd flip (k=2): A = [4, 1, 2, 3]
After 3rd flip (k=4): A = [3, 2, 1, 4]
After 4th flip (k=3): A = [1, 2, 3, 4], which is sorted. 


Example 2:

Input: [1,2,3]
Output: []
Explanation: The input is already sorted, so there is no need to flip anything.
Note that other answers, such as [3, 3], would also be accepted.


Note:
	1 <= A.length <= 100
	A[i] is a permutation of [1, 2, ..., A.length]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem969 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (PancakeSort([3, 2, 4, 1]));
            Console.WriteLine (PancakeSort([1, 2, 3]));
            Console.WriteLine (PancakeSort([5, 4, 3, 2, 1]));
        }

        // Time: O (n^2)
        // Space: O (n)
        public static IList<int> PancakeSort (int[] A) {
            IList<int> result = [];

            if (A == null || A.Length == 0) {
                return result;
            }

            for (var i = A.Length; i >= 0; i--) {
                var idx = Array.IndexOf (A, i);

                if (idx != i - 1) {
                    if (idx != 0) {
                        Reverse(A, idx + 1);
                        result.Add (idx + 1);
                    }

                    Reverse(A, i);
                    result.Add (i);
                }
            }

            return result;
        }

        private static void Reverse (int[] A, int k) {
            for (var i = 0; i < k / 2; i++) {
                var temp = A[i];
                A[i] = A[k - i - 1];
                A[k - i - 1] = temp;
            }
        }
    }
}