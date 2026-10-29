/*
One Away: 

There are three types of edits that can be performed on strings: insert a character,
remove a character, or replace a character. Given two strings, write a function to check if they are
one edit (or zero edits) away.

EXAMPLE
pale, ple -> true
pales, pale -> true
pale, bale -> true
pale, bake -> false
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c1q5 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (OneAway ("pale", "ple"));   // true
            Console.WriteLine (OneAway ("pales", "pale")); // true
            Console.WriteLine (OneAway ("pale", "bale"));  // true
            Console.WriteLine (OneAway ("ale", "bale"));   // true
            Console.WriteLine (OneAway ("xale", "xal"));   // true
            Console.WriteLine (OneAway ("pale", "bake"));  // false
        }

        public static bool OneAway (string a, string b) {
            if (a == null || b == null) {
                return false;
            }

            if (a.Length < b.Length) {
                return OneInsert (a, b);
            } else if (a.Length > b.Length) {
                return OneInsert (b, a);
            } else {
                return OneReplace (a, b);
            }
        }

        private static bool OneReplace (string a, string b) {
            int differences = 0;
            for (int i = 0; i < a.Length; i++) {
                if (a[i] != b[i]) {
                    differences++;
                }
                if (differences > 1) {
                    return false;
                }
            }
            return true;
        }

        private static bool OneInsert (string shorter, string longer) {
            if (longer.Length - shorter.Length > 1) {
                return false;
            }

            int i = 0;
            int j = 0;
            int differences = 0;

            while (i < shorter.Length && j < longer.Length) {
                if (shorter[i] != longer[j]) {
                    differences++;
                    if (differences > 1) {
                        return false;
                    }
                    j++;
                } else {
                    i++;
                    j++;
                }
            }

            return true;
        }
    }
}
