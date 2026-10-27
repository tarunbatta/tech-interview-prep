// Url:https://leetcode.com/problems/the-earliest-moment-when-everyone-become-friends

/*
1101. The Earliest Moment When Everyone Become Friends
Medium

There are n people in a social group labeled from 0 to n - 1. You are given an array logs where logs[i] = [timestampi, xi, yi] indicates that xi and yi will be friends at the time timestampi.

Friendship is symmetric and transitive.

Return the earliest timestamp for which every person became acquaintances with every other person in the group. If there is no such earliest time, return -1.

Example 1:
Input: logs = [[20190101,0,1],[20190104,3,4],[20190107,2,3],[20190211,1,5],[20190224,2,4],[20190301,0,3],[20190312,1,2],[20190322,4,5]], n = 6
Output: 20190301
Explanation: 
The first event occurs at timestamp = 20190101, and after 0 and 1 become friends, we have the following friendship groups [0,1], [2], [3], [4], [5].
The second event occurs at timestamp = 20190104, and after 3 and 4 become friends, we have the following friendship groups [0,1], [2], [3,4], [5].
The third event occurs at timestamp = 20190107, and after 2 and 3 become friends, we have the following friendship groups [0,1], [2,3,4], [5].
The fourth event occurs at timestamp = 20190211, and after 1 and 5 become friends, we have the following friendship groups [0,1,5], [2,3,4].
The fifth event occurs at timestamp = 20190224, and as 2 and 4 are already friends, nothing changes.
The sixth event occurs at timestamp = 20190301, and after 0 and 3 become friends, we all become friends.

Example 2:
Input: logs = [[0,2,0],[1,0,1],[3,0,3],[4,1,2],[7,3,1]], n = 4
Output: 3

Constraints:
- 2 <= n <= 100
- 1 <= logs.length <= 10^4
- logs[i].length == 3
- 0 <= timestampi <= 10^9
- 0 <= xi, yi <= n - 1
- xi != yi
- All the values timestampi are unique.
- All the pairs (xi, yi) will occur at most once in the given array.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1101 {
    public class Solution {
        public void Init() {
            int[][] logs1 = [
                [20190101, 0, 1], [20190104, 3, 4], [20190107, 2, 3],
                [20190211, 1, 5], [20190224, 2, 4], [20190301, 0, 3],
                [20190312, 1, 2], [20190322, 4, 5]
            ];
            Console.WriteLine(EarliestAcq(logs1, 6)); // 20190301

            int[][] logs2 = [
                [0, 2, 0], [1, 0, 1], [3, 0, 3], [4, 1, 2], [7, 3, 1]
            ];
            Console.WriteLine(EarliestAcq(logs2, 4)); // 3
        }

        // Time: O(L log L + L * alpha(n)) where L is logs length and n is number of people
        // Space: O(n) for DSU parent array
        public int EarliestAcq(int[][] logs, int n) {
            Array.Sort(logs, (a, b) => a[0].CompareTo(b[0]));

            int[] parent = new int[n];
            for (int i = 0; i < n; i++) {
                parent[i] = i;
            }

            int components = n;

            int Find(int i) {
                if (parent[i] != i) {
                    parent[i] = Find(parent[i]);
                }
                return parent[i];
            }

            bool Union(int i, int j) {
                int rootI = Find(i);
                int rootJ = Find(j);
                if (rootI != rootJ) {
                    parent[rootI] = rootJ;
                    return true;
                }
                return false;
            }

            foreach (var log in logs) {
                int time = log[0];
                int x = log[1];
                int y = log[2];

                if (Union(x, y)) {
                    components--;
                    if (components == 1) {
                        return time;
                    }
                }
            }

            return -1;
        }
    }
}