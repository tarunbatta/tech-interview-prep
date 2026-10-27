// Url: https://leetcode.com/problems/valid-anagram/

/*
242. Valid Anagram
Easy

Given two strings s and t , write a function to determine if t is an anagram of s.

Example 1:

Input: s = "anagram", t = "nagaram"
Output: true
Example 2:

Input: s = "rat", t = "car"
Output: false
Note:
You may assume the string contains only lowercase alphabets.

Follow up:
What if the inputs contain unicode characters? How would you adapt your solution to such case?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem242 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsAnagram_a(null, null));
            Console.WriteLine (IsAnagram_a(null, "nagaram"));
            Console.WriteLine (IsAnagram_a("anagram", null));
            Console.WriteLine (IsAnagram_a("anagram", "nagaram"));
            Console.WriteLine (IsAnagram_a("rat", "car"));

            Console.WriteLine (IsAnagram_b(null, null));
            Console.WriteLine (IsAnagram_b("anagram", "nagaram"));
            Console.WriteLine (IsAnagram_b("rat", "car"));
        }

        public static bool IsAnagram_a (string s, string t) {
            if ((s == null && t != null) || (s != null && t == null) || (s != null && t != null && s.Length != t.Length)) {
                return false;
            }

            bool result = true;
            char[] sArray = s.ToCharArray ();
            char[] tArray = t.ToCharArray ();

            Array.Sort (sArray);
            Array.Sort (tArray);

            for (int i = 0; i < sArray.Length; i++) {
                if (sArray[i] != tArray[i]) {
                    return false;
                }
            }

            return result;
        }

        public static bool IsAnagram_b (string s, string t) {
            if ((s == null && t != null) || (s != null && t == null) || (s != null && t != null && s.Length != t.Length)) {
                return false;
            }

            bool result = true;
            int[] count = new int[26];

            for (int i = 0; i < s.Length; i++) {
                count[s[i] - 'a']++;
                count[t[i] - 'a']--;
            }

            for (int i = 0; i < count.Length; i++) {
                if (count[i] != 0) {
                    return false;
                }
            }

            return result;
        }
    }
}