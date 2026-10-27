// Url:https://leetcode.com/problems/kill-process

/*
582. Kill Process
Medium

You have n processes, each process has a unique PID (process id) and its PPID (parent process id).

Each process will have only one parent process, but may have one or more children processes. This is just like a tree structure. Only one process has PPID that is 0, which means this process has no parent process. All the PIDs will be distinct positive integers.

We use two list of integers to represent a list of processes, where the first list contains PID for each process and the second list contains the corresponding PPID.

Now given the two lists, and a PID representing a process you want to kill, return a list of PIDs of processes that will be killed in the end. You should assume that when a process is killed, all its children processes will be killed. No order is required for the final answer.

Example 1:
Input: 
pid =  [1, 3, 10, 5]
ppid = [3, 0, 5, 3]
kill = 5
Output: [5, 10]
Explanation: 
           3
         /   \
        1     5
             /
            10
Kill 5 will also kill 10.

Constraints:
- n == pid.length == ppid.length
- 1 <= n <= 5 * 10^4
- 1 <= pid[i] <= 5 * 10^4
- 0 <= ppid[i] <= 5 * 10^4
- All the values of pid are unique.
- kill is guaranteed to be in pid.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem582 {
    public class Solution {
        public void Init() {
            IList<int> pid1 = [1, 3, 10, 5];
            IList<int> ppid1 = [3, 0, 5, 3];
            int kill1 = 5;
            Console.WriteLine(string.Join(", ", KillProcess(pid1, ppid1, kill1))); // 5, 10

            IList<int> pid2 = [1];
            IList<int> ppid2 = [0];
            int kill2 = 1;
            Console.WriteLine(string.Join(", ", KillProcess(pid2, ppid2, kill2))); // 1
        }

        // Time: O(n) where n is the number of processes
        // Space: O(n) to store the tree and queue/result
        public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill) {
            Dictionary<int, List<int>> children = [];
            for (int i = 0; i < pid.Count; i++) {
                int p = pid[i];
                int parent = ppid[i];
                if (!children.TryGetValue(parent, out var list)) {
                    list = [];
                    children[parent] = list;
                }
                list.Add(p);
            }

            List<int> result = [];
            Queue<int> queue = new();
            queue.Enqueue(kill);

            while (queue.Count > 0) {
                int curr = queue.Dequeue();
                result.Add(curr);

                if (children.TryGetValue(curr, out var kids)) {
                    foreach (int child in kids) {
                        queue.Enqueue(child);
                    }
                }
            }

            return result;
        }
    }
}