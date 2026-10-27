// Url: https://leetcode.com/problems/encode-and-decode-strings

/*
271. Encode and Decode Strings
Medium

Design an algorithm to encode a list of strings to a string. The encoded string is then sent over the network and is decoded back to the original list of strings.

Please implement encode and decode

Example 1:
Input: ["lint","code","love","you"]
Output: ["lint","code","love","you"]
Explanation:
One possible encode method is: "lint:;code:;love:;you"

Example 2:
Input: ["we", "say", ":", "yes"]
Output: ["we", "say", ":", "yes"]
Explanation:
One possible encode method is: "we:;say:;:::;yes"

Constraints:
0 <= strs.length <= 200
0 <= strs[i].length <= 200
strs[i] contains any possible characters out of 256 valid ASCII characters.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem271
{
    public class Solution
    {
        public void Init()
        {
            Codec codec = new();
            List<string> input = new() { "lint", "code", "love", "you" };
            string encoded = codec.Encode(input);
            Console.WriteLine(encoded); // 4#lint4#code4#love3#you

            IList<string> decoded = codec.Decode(encoded);
            Console.WriteLine(string.Join(", ", decoded)); // lint, code, love, you
        }
    }

    // Time:
    //   Encode: O(N) where N is the total number of characters in all strings
    //   Decode: O(N)
    // Space: O(1) auxiliary space (excluding the output encoded string and decoded list)
    public class Codec
    {
        // Encodes a list of strings to a single string using length-prefix encoding.
        public string Encode(IList<string> strs)
        {
            if (strs == null || strs.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new();

            foreach (string s in strs)
            {
                sb.Append(s.Length);
                sb.Append('#');
                sb.Append(s);
            }

            return sb.ToString();
        }

        // Decodes a single string to a list of strings.
        public IList<string> Decode(string s)
        {
            List<string> result = new();

            if (string.IsNullOrEmpty(s))
            {
                return result;
            }

            int i = 0;

            while (i < s.Length)
            {
                int delimiterIndex = s.IndexOf('#', i);

                if (delimiterIndex == -1)
                {
                    break;
                }

                int length = int.Parse(s.Substring(i, delimiterIndex - i));
                string str = s.Substring(delimiterIndex + 1, length);
                result.Add(str);

                i = delimiterIndex + 1 + length;
            }

            return result;
        }
    }
}