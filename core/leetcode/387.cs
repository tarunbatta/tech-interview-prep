// Url: https://leetcode.com/problems/first-unique-character-in-a-string/

/*
387. First Unique Character in a String
Easy

Given a string, find the first non-repeating character in it and return it's index. If it doesn't exist, return -1.

Examples:

s = "leetcode"
return 0.

s = "loveleetcode",
return 2.
Note: You may assume the string contain only lowercase letters.
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem387 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (FirstUniqChar(null));
            Console.WriteLine (FirstUniqChar("leetcode"));
            Console.WriteLine (FirstUniqChar("loveleetcode"));
        }

        public static int FirstUniqChar (string s) {
            int result = -1;

            if (s == null || s.Length == 0) {
                return result;
            }

            if (s.Length == 0) {
                return 0;
            }

            Hashtable hash = [];
            for (int i = 0; i < s.Length; i++) {
                if (!hash.ContainsKey (s[i])) {
                    hash.Add (s[i], 0);
                }

                hash[s[i]] = (int) hash[s[i]] + 1;
            }

            for (int i = 0; i < s.Length; i++) {
                if ((int) hash[s[i]] == 1) {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}