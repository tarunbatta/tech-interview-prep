// Url: https://leetcode.com/problems/intersection-of-two-arrays-ii/

/*
350. Intersection of Two Arrays II
Easy

Given two arrays, write a function to compute their intersection.

Example 1:
Input: nums1 = [1,2,2,1], nums2 = [2,2]
Output: [2,2]

Example 2:
Input: nums1 = [4,9,5], nums2 = [9,4,9,8,4]
Output: [4,9]

Note:
Each element in the result should appear as many times as it shows in both arrays.
The result can be in any order.

Follow up:
What if the given array is already sorted? How would you optimize your algorithm?
What if nums1's size is small compared to nums2's size? Which algorithm is better?
What if elements of nums2 are stored on disk, and the memory is limited such that you cannot load all elements into the memory at once?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem350 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (Intersect(null, null));
            Console.WriteLine (Intersect(null, [2, 2]));
            Console.WriteLine (Intersect([1, 2, 2, 1], null));
            Console.WriteLine (Intersect([1, 2, 2, 1], [2, 2]));
            Console.WriteLine (Intersect([4, 9, 5], [9, 4, 9, 8, 4]));
        }

        public static int[] Intersect (int[] nums1, int[] nums2) {
            var dict = new Dictionary<int, int> ();
            Array.Sort (nums1);
            Array.Sort (nums2);
            int i = 0;
            int j = 0;
            int count = 0;
            while (i < nums1.Length && j < nums2.Length) {
                if (nums1[i] < nums2[j]) {
                    i++;
                } else if (nums1[i] > nums2[j]) {
                    j++;
                } else {

                    if (dict.ContainsKey (nums1[i])) {
                        dict[nums1[i]] += 1;
                    } else {
                        dict.Add (nums1[i], 1);
                    }
                    count++;
                    i++;
                    j++;
                }
            }

            int k = 0;
            int[] result = new int[count];
            foreach (var item in dict) {
                int c = item.Value;
                while (c > 0) {
                    result[k] = item.Key;
                    c--;
                    k++;
                }
            }

            return result;
        }
    }
}