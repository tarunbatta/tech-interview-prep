// Url:https://leetcode.com/problems/analyze-user-website-visit-pattern

/*
1152. Analyze User Website Visit Pattern
Medium

You are given two string arrays username and website and an integer array timestamp. All the given arrays are of the same length and the tuple [username[i], timestamp[i], website[i]] indicates that the user username[i] visited the website website[i] at time timestamp[i].

A 3-sequence is a list of three websites (not necessarily distinct).
- For example, ["portal", "carrier", "notes"], ["ocean", "code", "notes"], etc.

The score of a 3-sequence is the number of users that visited all the websites in the sequence in that same order.

Return the 3-sequence with the highest score. If there is more than one 3-sequence with the same highest score, return the lexicographically smallest such sequence.

Example 1:
Input: username = ["joe","joe","joe","james","james","james","james","mary","mary","mary"], timestamp = [1,2,3,4,5,6,7,8,9,10], website = ["home","about","career","home","cart","maps","home","home","about","career"]
Output: ["home","about","career"]
Explanation: The tuples in this example are:
["joe", 1, "home"]
["joe", 2, "about"]
["joe", 3, "career"]
["james", 4, "home"]
["james", 5, "cart"]
["james", 6, "maps"]
["james", 7, "home"]
["mary", 8, "home"]
["mary", 9, "about"]
["mary", 10, "career"]
The 3-sequence ("home", "about", "career") was visited at least once by 2 users.
The 3-sequence ("home", "cart", "maps") was visited at least once by 1 user.
The 3-sequence ("home", "cart", "home") was visited at least once by 1 user.
The 3-sequence ("home", "maps", "home") was visited at least once by 1 user.
The 3-sequence ("cart", "maps", "home") was visited at least once by 1 user.
The 3-sequence ("home", "about", "career") has the highest score of 2.

Example 2:
Input: username = ["ua","ua","ua","ub","ub","ub"], timestamp = [1,2,3,4,5,6], website = ["a","b","a","a","b","c"]
Output: ["a","b","a"]

Constraints:
- 3 <= username.length <= 50
- 1 <= username[i].length <= 10
- timestamp.length == username.length
- 1 <= timestamp[i] <= 10^9
- website.length == username.length
- 1 <= website[i].length <= 10
- username[i] and website[i] consist of lowercase English letters.
- It is guaranteed that there is at least one user who visited at least three websites.
- All the tuples [username[i], timestamp[i], website[i]] are distinct.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1152 {
    public class Solution {
        public static void Init() {
            string[] u1 = ["joe", "joe", "joe", "james", "james", "james", "james", "mary", "mary", "mary"];
            int[] t1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            string[] w1 = ["home", "about", "career", "home", "cart", "maps", "home", "home", "about", "career"];
            Console.WriteLine(string.Join(", ", MostVisitedPattern(u1, t1, w1))); // "home", "about", "career"

            string[] u2 = ["ua", "ua", "ua", "ub", "ub", "ub"];
            int[] t2 = [1, 2, 3, 4, 5, 6];
            string[] w2 = ["a", "b", "a", "a", "b", "c"];
            Console.WriteLine(string.Join(", ", MostVisitedPattern(u2, t2, w2))); // "a", "b", "a"
        }

        // Time: O(U * L^3 + P log P) where U is users, L is websites per user
        // Space: O(U * L^3) for storing 3-sequences
        public static IList<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website) {
            int n = username.Length;
            var visits = new List<(string User, int Time, string Site)>(n);

            for (int i = 0; i < n; i++) {
                visits.Add((username[i], timestamp[i], website[i]));
            }

            var userVisits = visits
                .GroupBy(v => v.User)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(v => v.Time).Select(v => v.Site).ToList()
                );

            Dictionary<(string, string, string), int> patternScores = [];

            foreach (var sites in userVisits.Values) {
                if (sites.Count < 3) {
                    continue;
                }

                HashSet<(string, string, string)> userPatterns = [];
                for (int i = 0; i < sites.Count - 2; i++) {
                    for (int j = i + 1; j < sites.Count - 1; j++) {
                        for (int k = j + 1; k < sites.Count; k++) {
                            userPatterns.Add((sites[i], sites[j], sites[k]));
                        }
                    }
                }

                foreach (var pattern in userPatterns) {
                    patternScores[pattern] = patternScores.GetValueOrDefault(pattern, 0) + 1;
                }
            }

            var best = patternScores
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key.Item1, StringComparer.Ordinal)
                .ThenBy(kvp => kvp.Key.Item2, StringComparer.Ordinal)
                .ThenBy(kvp => kvp.Key.Item3, StringComparer.Ordinal)
                .First()
                .Key;

            return [best.Item1, best.Item2, best.Item3];
        }
    }
}