/*
Word Frequencies: 

Design a method to find the frequency of occurrences of any given word in a
book. What if we were running this algorithm multiple times?
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InterviewPreperationGuide.Core.Ctci.c16q2 {
    public class Solution {
        private static Dictionary<string, int> _wordMap = new Dictionary<string, int> (StringComparer.OrdinalIgnoreCase);

        public static void SetUpHashTable (string text) {
            _wordMap.Clear ();
            string clean = Regex.Replace (text, @"[^A-Za-z0-9]", " ");
            string[] words = clean.Split (' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words) {
                if (_wordMap.ContainsKey (word)) {
                    _wordMap[word]++;
                } else {
                    _wordMap[word] = 1;
                }
            }
        }

        public static int GetFrequency (string word) {
            return _wordMap.TryGetValue (word, out int count) ? count : 0;
        }

        public static void Init (string[] args = null) {
            string book = "Hello this is a test line supposedly representing a book. The book is very short.";
            SetUpHashTable (book);
            Console.WriteLine ($"Frequency of 'book': {GetFrequency ("book")}"); // 2
            Console.WriteLine ($"Frequency of 'is': {GetFrequency ("is")}");     // 2
            Console.WriteLine ($"Frequency of 'missing': {GetFrequency ("missing")}"); // 0
        }
    }
}
