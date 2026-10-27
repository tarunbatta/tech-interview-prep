// Url:https://leetcode.com/problems/parallel-courses

/*
1136. Parallel Courses
Medium

You are given an integer n, which indicates that there are n courses labeled from 1 to n. You are also given an array relations where relations[i] = [prevCoursei, nextCoursei], representing a prerequisite relationship between course prevCoursei and course nextCoursei: course prevCoursei has to be taken before course nextCoursei.

In one semester, you can take any number of courses as long as you have taken all the prerequisites in the previous semester for the courses you are taking.

Return the minimum number of semesters needed to take all courses. If there is no way to take all the courses, return -1.

Example 1:
Input: n = 3, relations = [[1,3],[2,3]]
Output: 2
Explanation: The figure above represents the given graph.
In the first semester, you can take courses 1 and 2.
In the second semester, you can take course 3.

Example 2:
Input: n = 3, relations = [[1,2],[2,3],[3,1]]
Output: -1
Explanation: No course can be taken because they depend on each other.

Constraints:
- 1 <= n <= 5000
- 1 <= relations.length <= 5000
- relations[i].length == 2
- 1 <= prevCoursei, nextCoursei <= n
- prevCoursei != nextCoursei
- All the pairs [prevCoursei, nextCoursei] are unique.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1136 {
    public class Solution {
        public void Init() {
            int[][] rel1 = [[1, 3], [2, 3]];
            Console.WriteLine(MinimumSemesters(3, rel1)); // 2

            int[][] rel2 = [[1, 2], [2, 3], [3, 1]];
            Console.WriteLine(MinimumSemesters(3, rel2)); // -1
        }

        // Time: O(V + E) where V is n and E is relations length
        // Space: O(V + E) for graph adjacency list and queue
        public int MinimumSemesters(int n, int[][] relations) {
            List<int>[] graph = new List<int>[n + 1];
            int[] inDegree = new int[n + 1];

            for (int i = 1; i <= n; i++) {
                graph[i] = [];
            }

            foreach (var rel in relations) {
                graph[rel[0]].Add(rel[1]);
                inDegree[rel[1]]++;
            }

            Queue<int> queue = new();
            for (int i = 1; i <= n; i++) {
                if (inDegree[i] == 0) {
                    queue.Enqueue(i);
                }
            }

            int semesters = 0;
            int coursesTaken = 0;

            while (queue.Count > 0) {
                int levelSize = queue.Count;
                semesters++;

                for (int i = 0; i < levelSize; i++) {
                    int curr = queue.Dequeue();
                    coursesTaken++;

                    foreach (int next in graph[curr]) {
                        inDegree[next]--;
                        if (inDegree[next] == 0) {
                            queue.Enqueue(next);
                        }
                    }
                }
            }

            return coursesTaken == n ? semesters : -1;
        }
    }
}