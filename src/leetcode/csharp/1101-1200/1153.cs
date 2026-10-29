namespace InterviewPreperationGuide.Core.LeetCode.problem1153
{
    using System;
    using System.Collections.Generic;

    // 1153. String Transforms Into Another String
    // https://leetcode.com/problems/string-transforms-into-another-string/
    //
    // Given two strings str1 and str2 of the same length, determine whether you can transform str1
    // into str2 by doing zero or more conversions.
    //
    // In one conversion you can convert all occurrences of one character in str1 to any other lowercase English character.
    //
    // Return true if and only if you can transform str1 into str2.
    //
    // Example 1:
    // Input: str1 = "aabcc", str2 = "ccdee"
    // Output: true
    // Explanation: Convert 'c' to 'e', then 'b' to 'd', then 'a' to 'c'. Note that the order of conversions matters.
    //
    // Example 2:
    // Input: str1 = "leetcode", str2 = "codeleet"
    // Output: false
    // Explanation: There is no way to transform str1 into str2.
    //
    // Constraints:
    // 1 <= str1.length == str2.length <= 10^4
    // str1 and str2 consist of only lowercase English letters.
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(CanConvert("aabcc", "ccdee")); // true
            Console.WriteLine(CanConvert("leetcode", "codeleet")); // false
            Console.WriteLine(CanConvert("abcdefghijklmnopqrstuvwxyz", "bcdefghijklmnopqrstuvwxyza")); // false (26 unique chars in target)
            Console.WriteLine(CanConvert("aa", "cd")); // false (same char maps to two different chars)
            Console.WriteLine(CanConvert("paper", "title")); // true
        }

        // Time: O(n) where n is the length of str1
        // Space: O(1) since the alphabet size is at most 26
        public static bool CanConvert(string str1, string str2)
        {
            if (str1 == str2)
            {
                return true;
            }

            var map = new Dictionary<char, char>();
            for (int i = 0; i < str1.Length; i++)
            {
                char c1 = str1[i];
                char c2 = str2[i];

                if (map.TryGetValue(c1, out char mapped))
                {
                    if (mapped != c2)
                    {
                        return false;
                    }
                }
                else
                {
                    map[c1] = c2;
                }
            }

            // If str2 has all 26 distinct characters, there is no spare character to use
            // as an intermediate step to break cycles.
            var uniqueInStr2 = new HashSet<char>(str2);
            return uniqueInStr2.Count < 26;
        }
    }
}