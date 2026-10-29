// Url:https://leetcode.com/problems/reorder-data-in-log-files

/*
937. Reorder Data in Log Files
Easy

You have an array of logs.  Each log is a space delimited string of words.

For each log, the first word in each log is an alphanumeric identifier.  Then, either:


	Each word after the identifier will consist only of lowercase letters, or;
	Each word after the identifier will consist only of digits.


We will call these two varieties of logs letter-logs and digit-logs.  It is guaranteed that each log has at least one word after its identifier.

Reorder the logs so that all of the letter-logs come before any digit-log.  The letter-logs are ordered lexicographically ignoring identifier, with the identifier used in case of ties.  The digit-logs should be put in their original order.

Return the final order of the logs.

 
Example 1:
Input: logs = ["dig1 8 1 5 1","let1 art can","dig2 3 6","let2 own kit dig","let3 art zero"]
Output: ["let1 art can","let3 art zero","let2 own kit dig","dig1 8 1 5 1","dig2 3 6"]

 
Constraints:
	0 <= logs.length <= 100
	3 <= logs[i].length <= 100
	logs[i] is guaranteed to have an identifier, and a word after the identifier.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem937 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (ReorderLogFiles(null));
            Console.WriteLine (ReorderLogFiles([]));
            Console.WriteLine (ReorderLogFiles(["dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero"]));
        }

        public static string[] ReorderLogFiles (string[] logs) {
            if (logs == null || logs.Length == 0) {
                return logs;
            }

            List<string> letters = [];
            List<string> digits = [];

            foreach (string log in logs) {
                string[] words = log.Split (' ');
                bool isDigit = char.IsDigit (words[1][0]);

                if (isDigit) {
                    digits.Add (log);
                } else {
                    letters.Add (log);
                }
            }

            letters.Sort (new LetterLogComparer ());

            letters.AddRange (digits);

            return [.. letters];
        }
    }

    public class LetterLogComparer : IComparer<string> {
        public int Compare (string one, string two) {
            var oneWithoutIdentifier = RemoveIdentifier(one);
            var twoWithoutIdentifier = RemoveIdentifier(two);

            if (oneWithoutIdentifier.Equals (twoWithoutIdentifier)) {
                // a match was found, so compare the original strings with the identifier included
                return string.Compare (one, two);
            }

            return string.Compare (oneWithoutIdentifier, twoWithoutIdentifier);
        }

        private static string RemoveIdentifier (string str) {
            var firstSpace = str.IndexOf (" ");
            return str.Substring (firstSpace + 1, str.Length - firstSpace - 1);
        }
    }
}