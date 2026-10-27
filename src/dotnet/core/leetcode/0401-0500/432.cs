// Url:https://leetcode.com/problems/all-oone-data-structure

/*
432. All O`one Data Structure
Hard

Implement a data structure supporting the following operations:



Inc(Key) - Inserts a new key  with value 1. Or increments an existing key by 1. Key is guaranteed to be a non-empty string.
Dec(Key) - If Key's value is 1, remove it from the data structure. Otherwise decrements an existing key by 1. If the key does not exist, this function does nothing. Key is guaranteed to be a non-empty string.
GetMaxKey() - Returns one of the keys with maximal value. If no element exists, return an empty string "".
GetMinKey() - Returns one of the keys with minimal value. If no element exists, return an empty string "".




Challenge: Perform all these in O(1) time complexity.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem432 {
    public class Solution {
        public static void Init () {
            AllOne obj = new();
            AllOne.Inc(null);
            AllOne.Dec(null);
            string param_3 = AllOne.GetMaxKey();
            string param_4 = AllOne.GetMinKey();
        }
    }

    public class AllOne {
        public AllOne () {

        }

        // Inserts a new key <Key> with value 1. Or increments an existing key by 1
        public static void Inc (string key) {

        }

        // Decrements an existing key by 1. If Key's value is 1, remove it from the data structure
        public static void Dec (string key) {

        }

        //  Returns one of the keys with maximal value
        public static string GetMaxKey () {
            return null;
        }

        //  Returns one of the keys with Minimal value
        public static string GetMinKey () {
            return null;
        }
    }
}