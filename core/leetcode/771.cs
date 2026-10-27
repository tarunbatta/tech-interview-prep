// Url: https://leetcode.com/problems/jewels-and-stones/

/*
771. Jewels and Stones
Easy

You're given strings J representing the types of stones that are jewels, and S representing the stones you have.  Each character in S is a type of stone you have.  You want to know how many of the stones you have are also jewels.

The letters in J are guaranteed distinct, and all characters in J and S are letters. Letters are case sensitive, so "a" is considered a different type of stone from "A".

Example 1:

Input: J = "aA", S = "aAAbbbb"
Output: 3
Example 2:

Input: J = "z", S = "ZZ"
Output: 0
Note:

S and J will consist of letters and have length at most 50.
The characters in J are distinct.
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem771 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (NumJewelsInStones(null, null));
            Console.WriteLine (NumJewelsInStones(null, "aAAbbbb"));
            Console.WriteLine (NumJewelsInStones("aA", null));
            Console.WriteLine (NumJewelsInStones("aA", "aAAbbbb"));
            Console.WriteLine (NumJewelsInStones("z", "ZZ"));
        }

        public static int NumJewelsInStones (string J, string S) {
            int result = 0;

            if (string.IsNullOrEmpty (S) || string.IsNullOrEmpty (J)) {
                return result;
            }

            Hashtable hash = [];

            for (int i = 0; i < S.Length; i++) {
                if (!hash.ContainsKey (S[i])) {
                    hash.Add (S[i], 1);
                } else {
                    hash[S[i]] = (int) hash[S[i]] + 1;
                }
            }

            for (int i = 0; i < J.Length; i++) {
                if (hash.ContainsKey (J[i])) {
                    result += (int) hash[J[i]];
                }
            }

            return result;
        }
    }
}