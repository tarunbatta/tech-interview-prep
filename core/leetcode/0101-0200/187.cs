// Url:https://leetcode.com/problems/repeated-dna-sequences

/*
187. Repeated DNA Sequences
Medium

All DNA is composed of a series of nucleotides abbreviated as A, C, G, and T, for example: "ACGAATTCCG". When studying DNA, it is sometimes useful to identify repeated sequences within the DNA.

Write a function to find all the 10-letter-long sequences (substrings) that occur more than once in a DNA molecule.

Example:

Input: s = "AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT"

Output: ["AAAAACCCCC", "CCCCCAAAAA"]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem187 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ((n-l)l)
        // Space: O ((n-l)l)
        public static IList<string> FindRepeatedDnaSequences (string s) {
            IList<string> result = [];

            if (string.IsNullOrEmpty (s)) {
                return result;
            }

            int len = 10;
            Dictionary<string, int> hash = [];
            for (int i = 0; i < s.Length - len + 1; i++) {
                string str = s.Substring (i, len);

                if (!hash.ContainsKey (str)) {
                    hash.Add (str, 1);
                } else {
                    hash[str]++;
                }

                if (hash[str] == 2) {
                    result.Add (str);
                }
            }

            return result;
        }
    }
}