// Url:https://leetcode.com/problems/minimum-window-substring

/*
76. Minimum Window Substring
Hard

Given a string S and a string T, find the minimum window in S which will contain all the characters in T in complexity O(n).

Example:

Input: S = "ADOBECODEBANC", T = "ABC"
Output: "BANC"


Note:


	If there is no such window in S that covers all characters in T, return the empty string "".
	If there is such window, you are guaranteed that there will always be only one unique minimum window in S.
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem76 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MinWindow("ADOBECODEBANC", null));
            Console.WriteLine (MinWindow(null, "ABC"));
            Console.WriteLine (MinWindow("ADOBECODEBANC", "ABC"));
            Console.WriteLine (MinWindow("dabceabcg", "deg"));
            Console.WriteLine (MinWindow("abcdedefg", "deg"));
            Console.WriteLine (MinWindow("abcdedefg", "xyz"));
            Console.WriteLine (MinWindow("eaebeccdeefeggh", "eee"));
        }

        // Time: O (s+t)
        // Space: O (s+t)
        public static string MinWindow (string s, string t) {
            string result = string.Empty;

            if (!string.IsNullOrEmpty (s) && !string.IsNullOrEmpty (t)) {
                char[] arrS = s.ToCharArray ();
                char[] arrT = t.ToCharArray ();

                Hashtable hashT = [];

                for (int i = 0; i < arrT.Length; i++) {
                    if (!hashT.ContainsKey (arrT[i])) {
                        hashT.Add (arrT[i], 1);
                    } else {
                        hashT[arrT[i]] = (int) hashT[arrT[i]] + 1;
                    }
                }

                int start = 0;
                int minlen = arrS.Length + 1;
                int match = 0;

                Hashtable hashS = [];

                for (int i = 0; i < arrS.Length; i++) {
                    if (hashT.ContainsKey (arrS[i])) {
                        if (hashS.ContainsKey (arrS[i])) {
                            if ((int) hashS[arrS[i]] < (int) hashT[arrS[i]]) {
                                match++;
                            }

                            hashS[arrS[i]] = (int) hashS[arrS[i]] + 1;
                        } else {
                            hashS.Add (arrS[i], 1);
                            match++;
                        }
                    }

                    if (match == arrT.Length) {
                        while (!hashS.ContainsKey (arrS[start]) || (int) hashS[arrS[start]] > (int) hashT[arrS[start]]) {
                            if (hashS.ContainsKey (arrS[start]) && (int) hashS[arrS[start]] > (int) hashT[arrS[start]]) {
                                hashS[arrS[start]] = (int) hashS[arrS[start]] - 1;
                            }

                            start++;
                        }

                        int end = i + 1 - start;

                        if (end < minlen) {
                            result = s.Substring (start, end);
                            minlen = end;
                        }
                    }
                }
            }

            return result;
        }
    }
}