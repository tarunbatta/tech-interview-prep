/*
Permutations without Dups: 

Write a method to compute all permutations of a string of unique characters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c8q7 {
    public class Solution {
        public static List<string> Permute (string str) {
            if (str == null) return null;
            return PermuteHelper (str, str.Length);
        }

        private static List<string> PermuteHelper (string str, int i) {
            List<string> ret = new List<string> ();
            if (i == 0) {
                ret.Add (string.Empty);
            } else {
                List<string> prev = PermuteHelper (str, i - 1);
                foreach (string a in prev) {
                    List<string> lst = InsertInto (a, str[i - 1]);
                    ret.AddRange (lst);
                }
            }
            return ret;
        }

        private static List<string> InsertInto (string a, char b) {
            List<string> ret = new List<string> ();
            for (int i = 0; i < a.Length; i++) {
                string newString = a.Substring (0, i) + b + a.Substring (i);
                ret.Add (newString);
            }
            ret.Add (a + b);
            return ret;
        }

        public static void Init (string[] args = null) {
            List<string> perms = Permute ("abc");
            Console.WriteLine ($"Permutations of 'abc': {string.Join (", ", perms)}");
        }
    }
}
