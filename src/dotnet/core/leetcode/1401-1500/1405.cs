// Url: https://leetcode.com/problems/longest-happy-string/

/*
1405. Longest Happy String
Medium

A string s is called happy if it satisfies the following conditions:
- s only contains the letters 'a', 'b', and 'c'.
- s does not contain any of "aaa", "bbb", or "ccc" as a substring.
- s contains at most a occurrences of the letter 'a'.
- s contains at most b occurrences of the letter 'b'.
- s contains at most c occurrences of the letter 'c'.

Given three integers a, b, and c, return the longest possible happy string. If there are multiple longest happy strings, return any of them. An empty string "" should be returned if no such string can be formed.

Example 1:
Input: a = 1, b = 1, c = 7
Output: "ccaccbcc"
Explanation: "ccbccacc" would also be a correct answer.

Example 2:
Input: a = 7, b = 1, c = 0
Output: "aabaa"
Explanation: It is the only correct answer in this case.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem1405
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(LongestDiverseString(1, 1, 7)); // Expected: "ccaccbcc" (or valid happy string of length 8)
            Console.WriteLine(LongestDiverseString(7, 1, 0)); // Expected: "aabaa"
            Console.WriteLine(LongestDiverseString(0, 0, 0)); // Expected: ""
        }

        // Time: O(a + b + c)
        // Space: O(a + b + c) for the output string
        public static string LongestDiverseString(int a, int b, int c)
        {
            // Max heap ordering by remaining count descending
            PriorityQueue<(char ch, int count), int> maxHeap = new(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            if (a > 0)
            {
                maxHeap.Enqueue(('a', a), a);
            }

            if (b > 0)
            {
                maxHeap.Enqueue(('b', b), b);
            }

            if (c > 0)
            {
                maxHeap.Enqueue(('c', c), c);
            }

            StringBuilder sb = new();

            while (maxHeap.Count > 0)
            {
                var first = maxHeap.Dequeue();

                // If adding first.ch creates 3 consecutive identical characters
                if (sb.Length >= 2 && sb[sb.Length - 1] == first.ch && sb[sb.Length - 2] == first.ch)
                {
                    if (maxHeap.Count == 0)
                    {
                        break;
                    }

                    var second = maxHeap.Dequeue();
                    sb.Append(second.ch);
                    second.count--;

                    if (second.count > 0)
                    {
                        maxHeap.Enqueue(second, second.count);
                    }

                    maxHeap.Enqueue(first, first.count);
                }
                else
                {
                    sb.Append(first.ch);
                    first.count--;

                    if (first.count > 0)
                    {
                        maxHeap.Enqueue(first, first.count);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
