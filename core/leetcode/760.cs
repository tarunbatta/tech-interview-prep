// Url:https://leetcode.com/problems/find-anagram-mappings

/*
760. Find Anagram Mappings
Easy

You are given two integer arrays nums1 and nums2 where nums2 is an anagram of nums1. Both arrays may contain duplicates.

Return an index mapping array mapping from nums1 to nums2 where mapping[i] = j means the ith element in nums1 appears in nums2 at index j. If there are multiple answers, return any of them.

An array a is an anagram of an array b means b is made by randomizing the order of the elements in a.

Example 1:
Input: nums1 = [12,28,46,32,50], nums2 = [50,12,32,46,28]
Output: [1,4,3,2,0]
Explanation: As nums1[0] == 12 appears at nums2[1], mapping[0] = 1, nums1[1] == 28 appears at nums2[4], mapping[1] = 4, and so on.

Example 2:
Input: nums1 = [84,46], nums2 = [84,46]
Output: [0,1]

Constraints:
- 1 <= nums1.length <= 100
- nums2.length == nums1.length
- 0 <= nums1[i], nums2[i] <= 10^5
- nums2 is an anagram of nums1.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem760 {
    public class Solution {
        public void Init() {
            int[] nums1 = [12, 28, 46, 32, 50];
            int[] nums2 = [50, 12, 32, 46, 28];
            Console.WriteLine(string.Join(", ", AnagramMappings(nums1, nums2))); // 1, 4, 3, 2, 0
        }

        // Time: O(n) where n is the length of nums1 and nums2
        // Space: O(n) to store index mappings
        public int[] AnagramMappings(int[] nums1, int[] nums2) {
            Dictionary<int, List<int>> map = [];
            for (int j = 0; j < nums2.Length; j++) {
                if (!map.TryGetValue(nums2[j], out var list)) {
                    list = [];
                    map[nums2[j]] = list;
                }
                list.Add(j);
            }

            int[] mapping = new int[nums1.Length];
            for (int i = 0; i < nums1.Length; i++) {
                var list = map[nums1[i]];
                mapping[i] = list[^1];
                list.RemoveAt(list.Count - 1);
            }

            return mapping;
        }
    }
}