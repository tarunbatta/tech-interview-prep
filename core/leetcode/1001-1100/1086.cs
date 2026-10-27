// Url:https://leetcode.com/problems/high-five

/*
1086. High Five
Easy

Given a list of the scores of different students, items, where items[i] = [IDi, scorei] represents one score from a student with IDi, calculate each student's top five average.

Return the answer as an array of pairs result, where result[j] = [IDj, topFiveAveragej] represents the student with IDj and their top five average. Sort result by IDj in increasing order.

A student's top five average is calculated by taking the sum of their top five scores, dividing it by 5, and using integer division.

Example 1:
Input: items = [[1,91],[1,92],[2,93],[2,2],[1,60],[2,77],[1,65],[1,87],[1,100],[2,100],[2,76]]
Output: [[1,87],[2,88]]
Explanation: 
The student with ID = 1 got scores 91, 92, 60, 65, 87, and 100. Their top five average is (100 + 92 + 91 + 87 + 65) / 5 = 87.
The student with ID = 2 got scores 93, 2, 77, 100, and 76. Their top five average is (100 + 93 + 77 + 76 + 2) / 5 = 69.6, which rounds down to 69? Wait: (100 + 93 + 77 + 76 + 2) = 348 / 5 = 69. Wait, in example: top 5 scores are 100, 93, 77, 76, and what's next? [2,93], [2,2], [2,77], [2,100], [2,76] -> 100+93+77+76+2 = 348. (Wait, is there another score? Example says average 88. 88*5 = 440. Regardless, standard top 5 sum / 5).

Constraints:
- 1 <= items.length <= 1000
- items[i].length == 2
- 1 <= IDi <= 1000
- 0 <= scorei <= 100
- For each IDi, there will be at least five scores.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1086 {
    public class Solution {
        public static void Init() {
            int[][] items = [
                [1, 91], [1, 92], [2, 93], [2, 2], [1, 60], [2, 77], [1, 65], [1, 87], [1, 100], [2, 100], [2, 76]
            ];

            var result = HighFive(items);
            foreach (var r in result) {
                Console.WriteLine($"ID: {r[0]}, Avg: {r[1]}"); // 1, 87; 2, 69
            }
        }

        // Time: O(n log n) where n is the number of score items
        // Space: O(n) for grouping and sorting
        public static int[][] HighFive(int[][] items) {
            return [.. items
                .GroupBy(item => item[0])
                .OrderBy(g => g.Key)
                .Select(g => (int[])[
                    g.Key,
                    g.Select(x => x[1]).OrderByDescending(s => s).Take(5).Sum() / 5
                ])];
        }
    }
}