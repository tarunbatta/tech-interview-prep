// Url:https://leetcode.com/problems/decode-string

/*
394. Decode String
Medium

Given an encoded string, return its decoded string.

The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times. Note that k is guaranteed to be a positive integer.

You may assume that the input string is always valid; No extra white spaces, square brackets are well-formed, etc.

Furthermore, you may assume that the original data does not contain any digits and that digits are only for those repeat numbers, k. For example, there won't be input like 3a or 2[4].

Examples:

s = "3[a]2[bc]", return "aaabcbc".
s = "3[a2[c]]", return "accaccacc".
s = "2[abc]3[cd]ef", return "abcabccdcdcdef".


 
*/

using System;

using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem394
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(DecodeString(null));               // Expected: ""
            Console.WriteLine(DecodeString(string.Empty));        // Expected: ""
            Console.WriteLine(DecodeString("3[a]2[bc]"));         // Expected: "aaabcbc"
            Console.WriteLine(DecodeString("3[a2[c]]"));          // Expected: "accaccacc"
            Console.WriteLine(DecodeString("2[abc]3[cd]ef"));     // Expected: "abcabccdcdcdef"
        }

        // Time: O(maxK * n) where n is length of s and maxK is maximum repeat count
        // Space: O(m) where m is the length of the decoded string
        public static string DecodeString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            Stack<int> countStack = new();
            Stack<StringBuilder> strStack = new();
            StringBuilder currentStr = new();
            int k = 0;

            foreach (char ch in s)
            {
                if (char.IsDigit(ch))
                {
                    k = (k * 10) + (ch - '0');
                }
                else if (ch == '[')
                {
                    countStack.Push(k);
                    strStack.Push(currentStr);
                    currentStr = new();
                    k = 0;
                }
                else if (ch == ']')
                {
                    int repeats = countStack.Pop();
                    StringBuilder prevStr = strStack.Pop();
                    for (int i = 0; i < repeats; i++)
                    {
                        prevStr.Append(currentStr);
                    }

                    currentStr = prevStr;
                }
                else
                {
                    currentStr.Append(ch);
                }
            }

            return currentStr.ToString();
        }
    }
}