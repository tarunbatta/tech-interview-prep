/*
Permutations with Dups: 

Write a method to compute all permutations of a string whose characters are not necessarily unique.
The list of permutations should not have duplicates.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c8q8 {
    public class Solution {
        public static HashSet<string> Permute (string str) {
            if (str == null) return null;
            return PermuteHelper (str, str.Length);
        }

        private static HashSet<string> PermuteHelper (string str, int i) {
            HashSet<string> ret = new HashSet<string> ();
            if (i == 0) {
                ret.Add (string.Empty);
            } else {
                HashSet<string> prev = PermuteHelper (str, i - 1);
                foreach (string a in prev) {
                    HashSet<string> lst = InsertInto (a, str[i - 1]);
                    foreach (string s in lst) {
                        ret.Add (s);
                    }
                }
            }
            return ret;
        }

        private static HashSet<string> InsertInto (string a, char b) {
            HashSet<string> ret = new HashSet<string> ();
            for (int i = 0; i < a.Length; i++) {
                string newString = a.Substring (0, i) + b + a.Substring (i);
                ret.Add (newString);
            }
            ret.Add (a + b);
            return ret;
        }

        public static void Init (string[] args = null) {
            HashSet<string> perms = Permute ("aab");
            Console.WriteLine ($"Permutations of 'aab': {string.Join (", ", perms)}");
        }
    }
}
