// Url: https://leetcode.com/problems/unique-word-abbreviation

/*
288. Unique Word Abbreviation
Medium

The abbreviation of a word is a concatenation of its first letter, the number of characters between the first and last letter, and its last letter. If a word has only two characters, then it is an abbreviation of itself.

For example:
- "dog" --> "d1g" because there is one letter between the first letter 'd' and the last letter 'g'.
- "internationalization" --> "i18n" because there are 18 letters between the first letter 'i' and the last letter 'n'.
- "it" --> "it" because any word with only two characters is an abbreviation of itself.

Implement the ValidWordAbbr class:
- ValidWordAbbr(String[] dictionary) Initializes the object with a dictionary of words.
- boolean isUnique(string word) Returns true if either of the following conditions are met (otherwise returns false):
  - There is no word in dictionary whose abbreviation is equal to word's abbreviation.
  - For any word in dictionary whose abbreviation is equal to word's abbreviation, that word and word are the same.

Example 1:
Input
["ValidWordAbbr", "isUnique", "isUnique", "isUnique", "isUnique", "isUnique"]
[[["deer", "door", "cake", "card"]], ["dear"], ["cart"], ["cane"], ["make"], ["cake"]]
Output
[null, false, true, false, true, true]

Explanation
ValidWordAbbr validWordAbbr = new ValidWordAbbr(["deer", "door", "cake", "card"]);
validWordAbbr.isUnique("dear"); // return false, dictionary word "deer" and "door" have the same abbreviation "d2r" but neither of them are "dear".
validWordAbbr.isUnique("cart"); // return true, no words in the dictionary have the abbreviation "c2t".
validWordAbbr.isUnique("cane"); // return false, dictionary word "cake" has the same abbreviation "c2e" and is not identical to "cane".
validWordAbbr.isUnique("make"); // return true, no words in the dictionary have the abbreviation "m2e".
validWordAbbr.isUnique("cake"); // return true, dictionary word "cake" has the same abbreviation "c2e" and is identical to "cake".

Constraints:
1 <= dictionary.length <= 3 * 10^4
1 <= dictionary[i].length <= 20
dictionary[i] consists of lowercase English letters.
1 <= word.length <= 20
word consists of lowercase English letters.
At most 5000 calls will be made to isUnique.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem288
{
    public class Solution
    {
        public void Init()
        {
            string[] dict = { "deer", "door", "cake", "card" };
            ValidWordAbbr vwa = new(dict);

            Console.WriteLine(vwa.IsUnique("dear")); // False
            Console.WriteLine(vwa.IsUnique("cart")); // True
            Console.WriteLine(vwa.IsUnique("cane")); // False
            Console.WriteLine(vwa.IsUnique("make")); // True
            Console.WriteLine(vwa.IsUnique("cake")); // True
        }
    }

    // Time:
    //   Constructor: O(N * L) where N is dictionary.Length and L is word length
    //   IsUnique: O(L)
    // Space: O(N * L) for storing abbreviations and unique words in hash maps
    public class ValidWordAbbr
    {
        private readonly Dictionary<string, HashSet<string>> _abbrMap;

        public ValidWordAbbr(string[] dictionary)
        {
            _abbrMap = new Dictionary<string, HashSet<string>>();

            if (dictionary == null)
            {
                return;
            }

            foreach (string word in dictionary)
            {
                string abbr = GetAbbreviation(word);

                if (!_abbrMap.TryGetValue(abbr, out HashSet<string> set))
                {
                    set = new HashSet<string>();
                    _abbrMap[abbr] = set;
                }

                set.Add(word);
            }
        }

        public bool IsUnique(string word)
        {
            string abbr = GetAbbreviation(word);

            if (!_abbrMap.TryGetValue(abbr, out HashSet<string> words))
            {
                return true;
            }

            return words.Count == 1 && words.Contains(word);
        }

        private static string GetAbbreviation(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length <= 2)
            {
                return word;
            }

            return $"{word[0]}{word.Length - 2}{word[^1]}";
        }
    }
}