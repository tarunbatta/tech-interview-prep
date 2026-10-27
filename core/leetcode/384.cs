// Url:https://leetcode.com/problems/shuffle-an-array

/*
384. Shuffle an Array
Medium

Shuffle a set of numbers without duplicates.


Example:
// Init an array with set 1, 2, and 3.
int[] nums = {1,2,3};
Solution solution = new Solution(nums);

// Shuffle the array [1,2,3] and return its result. Any permutation of [1,2,3] must equally likely to be returned.
solution.shuffle();

// Resets the array back to its original configuration [1,2,3].
solution.reset();

// Returns the random shuffling of array [1,2,3].
solution.shuffle();
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem384 {
    public class Solution(int[] nums)
    {
        public static void Init () {
            Solution obj = new([1, 2, 3]);
            int[] param_1 = obj.Shuffle ();
            int[] param_2 = obj.Reset ();
            int[] param_3 = obj.Shuffle ();
        }

        private int[] array = nums;
        private int[] original = (int[])nums.Clone();
        private Random rand = new();

        private List<int> getArrayCopy () {
            List<int> asList = [];

            for (int i = 0; i < array.Length; i++) {
                asList.Add (array[i]);
            }

            return asList;
        }

        // Resets the array to its original configuration and return it
        public int[] Reset () {
            array = original;
            original = (int[]) original.Clone ();
            return array;
        }

        // Returns a random shuffling of the array
        public int[] Shuffle () {
            List<int> aux = getArrayCopy ();

            for (int i = 0; i < array.Length; i++) {
                int removeIdx = rand.Next (aux.Count);
                array[i] = aux[removeIdx];
                aux.Remove (removeIdx);
            }

            return array;
        }
    }
}