// Url: https://leetcode.com/problems/sequence-reconstruction

/*
444. Sequence Reconstruction
Medium

You are given an integer array nums of length n where nums is a permutation of the integers in the range [1, n]. You are also given a 2D integer array sequences where sequences[i] is a subsequence of the integers in the range [1, n].

Check if nums is the shortest possible and the only supersequence. The shortest supersequence is a sequence with the shortest length and has all sequences[i] as subsequences. There could be multiple valid supersequences for the given array sequences.

- For example, for sequences = [[1,2],[1,3]], there are two shortest supersequences, [1,2,3] and [1,3,2].
- While for sequences = [[1,2],[1,3],[1,2,3]], the only shortest supersequence possible is [1,2,3]. [1,2,3,4] is a possible supersequence but not the shortest.

Return true if nums is the only shortest supersequence of sequences, or false otherwise.

Example 1:
Input: nums = [1,2,3], sequences = [[1,2],[1,3]]
Output: false
Explanation: There are two possible supersequences: [1,2,3] and [1,3,2].
The sequence [1,2] is a subsequence of both: [1,2,3] and [1,3,2].
The sequence [1,3] is a subsequence of both: [1,2,3] and [1,3,2].
Since nums is not the only shortest supersequence, we return false.

Example 2:
Input: nums = [1,2,3], sequences = [[1,2]]
Output: false
Explanation: The shortest possible supersequence is [1,2].
The sequence [1,2] is a subsequence of it: [1,2].
Since nums is not the shortest supersequence, we return false.

Example 3:
Input: nums = [1,2,3], sequences = [[1,2],[1,3],[2,3]]
Output: true
Explanation: The shortest possible supersequence is [1,2,3].
The sequence [1,2] is a subsequence of it: [1,2,3].
The sequence [1,3] is a subsequence of it: [1,2,3].
The sequence [2,3] is a subsequence of it: [1,2,3].
Since nums is the only shortest supersequence, we return true.

Constraints:
n == nums.length
1 <= n <= 10^4
nums is a permutation of all the integers in the range [1, n].
1 <= sequences.length <= 10^4
1 <= sequences[i].length <= 10^4
1 <= sum(sequences[i].length) <= 10^5
1 <= sequences[i][j] <= n
All the arrays of sequences are unique.
sequences[i] is a subsequence of nums.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem444
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(SequenceReconstruction([1, 2, 3], [[1, 2], [1, 3]]));                     // False
            Console.WriteLine(SequenceReconstruction([1, 2, 3], [[1, 2]]));                             // False
            Console.WriteLine(SequenceReconstruction([1, 2, 3], [[1, 2], [1, 3], [2, 3]]));             // True
        }

        // Time: O(V + E) where V is n and E is the total transitions in sequences
        // Space: O(V + E) for adjacency graph and in-degrees
        public static bool SequenceReconstruction(int[] nums, IList<IList<int>> sequences)
        {
            if (nums == null || sequences == null)
            {
                return false;
            }

            int n = nums.Length;
            Dictionary<int, HashSet<int>> graph = [];
            int[] inDegree = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {
                graph[i] = [];
            }

            foreach (var seq in sequences)
            {
                for (int i = 0; i < seq.Count - 1; i++)
                {
                    int u = seq[i];
                    int v = seq[i + 1];

                    if (graph[u].Add(v))
                    {
                        inDegree[v]++;
                    }
                }
            }

            Queue<int> queue = new();

            for (int i = 1; i <= n; i++)
            {
                if (inDegree[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            int index = 0;

            while (queue.Count > 0)
            {
                // If more than one node has 0 in-degree, the topological ordering is not unique
                if (queue.Count > 1)
                {
                    return false;
                }

                int curr = queue.Dequeue();

                if (index >= n || nums[index++] != curr)
                {
                    return false;
                }

                foreach (int next in graph[curr])
                {
                    inDegree[next]--;

                    if (inDegree[next] == 0)
                    {
                        queue.Enqueue(next);
                    }
                }
            }

            return index == n;
        }
    }
}