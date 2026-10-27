// Url:https://leetcode.com/problems/remove-vowels-from-a-string

/*
1119. Remove Vowels from a String
Easy

Given a string S, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.

 

Example 1:

Input: "leetcodeisacommunityforcoders"
Output: "ltcdscmmntyfrcdrs"
Example 2:

Input: "aeiou"
Output: ""

Note:
S consists of lowercase English letters only.
1 <= S.length <= 1000
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem1119 {
    public class Solution {
        public static void Init () {

        }

        // Time: O (n)
        // Space: O (1)
        public static string RemoveVowels (string S) {
            if (string.IsNullOrEmpty (S)) {
                return S;
            }

            HashSet<char> hash = ['a', 'e', 'i', 'o', 'u'];

            StringBuilder sb = new StringBuilder ();

            foreach (var item in S) {
                if (!hash.Contains (item)) {
                    sb.Append (item);
                }
            }

            return sb.ToString ();
        }
    }
}