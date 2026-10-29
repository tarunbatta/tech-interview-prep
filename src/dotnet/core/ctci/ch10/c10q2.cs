/*
Group Anagrams: 

Write a method to sort an array of strings so that all the anagrams are next to
each other.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c10q2 {
    public class Solution {
        public static void GroupAnagrams (string[] array) {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>> ();
            foreach (string s in array) {
                char[] chars = s.ToCharArray ();
                Array.Sort (chars);
                string key = new string (chars);
                if (!map.ContainsKey (key)) {
                    map[key] = new List<string> ();
                }
                map[key].Add (s);
            }

            int index = 0;
            foreach (var list in map.Values) {
                foreach (string s in list) {
                    array[index++] = s;
                }
            }
        }

        public static void Init (string[] args = null) {
            string[] words = { "cats", "acre", "atcs", "fried", "race", "axe", "exa", "care" };
            Console.WriteLine ($"Before: {string.Join (", ", words)}");
            GroupAnagrams (words);
            Console.WriteLine ($"After: {string.Join (", ", words)}");
        }
    }
}
