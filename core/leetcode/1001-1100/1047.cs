// Url:https://leetcode.com/problems/remove-all-adjacent-duplicates-in-string

/*
1047. Remove All Adjacent Duplicates In String
Easy

Given a string S of lowercase letters, a duplicate removal consists of choosing two adjacent and equal letters, and removing them.

We repeatedly make duplicate removals on S until we no longer can.

Return the final string after all such duplicate removals have been made.  It is guaranteed the answer is unique.

 

Example 1:

Input: "abbaca"
Output: "ca"
Explanation: 
For example, in "abbaca" we could remove "bb" since the letters are adjacent and equal, and this is the only possible move.  The result of this move is that the string is "aaca", of which only "aa" is possible, so the final string is "ca".


 

Note:


	1 <= S.length <= 20000
	S consists only of English lowercase letters.
*/

using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem1047
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(RemoveDuplicates(null));      // Expected: ""
            Console.WriteLine(RemoveDuplicates("abbaca"));  // Expected: "ca"
            Console.WriteLine(RemoveDuplicates("azxxzy"));  // Expected: "ay"

            // GEICO Follow-up variant tests:
            Console.WriteLine(ReduceLettersGeicoVariant(1));  // Expected: "a"
            Console.WriteLine(ReduceLettersGeicoVariant(2));  // Expected: "b"
            Console.WriteLine(ReduceLettersGeicoVariant(3));  // Expected: "ba"
            Console.WriteLine(ReduceLettersGeicoVariant(7));  // Expected: "cba"
        }

        // Time: O(n) where n is the length of s
        // Space: O(n) for the output StringBuilder stack
        public static string RemoveDuplicates(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            StringBuilder sb = new();

            foreach (char ch in s)
            {
                if (sb.Length > 0 && sb[^1] == ch)
                {
                    sb.Length--;
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        // GEICO Follow-up: Given n 'a's, any 'aa' becomes 'b', 'bb' becomes 'c', up to 'zz'
        // Time: O(log n)
        // Space: O(log n) for the output string
        public static string ReduceLettersGeicoVariant(long n)
        {
            if (n <= 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new();

            // 'z' corresponds to 2^25
            long zWeight = 1L << 25;
            long zCount = n / zWeight;
            long remainder = n % zWeight;

            if (zCount > 0)
            {
                sb.Append('z', (int)zCount);
            }

            // Read remaining bits from MSB (y = 2^24) down to LSB (a = 2^0)
            for (int bit = 24; bit >= 0; bit--)
            {
                if ((remainder & (1L << bit)) != 0)
                {
                    sb.Append((char)('a' + bit));
                }
            }

            return sb.ToString();
        }
    }
}