// Url: https://leetcode.com/problems/utf-8-validation/

/*
393. UTF-8 Validation
Medium

Given an integer array data representing the data, return whether it is a valid UTF-8 encoding (i.e. it translates to a sequence of valid UTF-8 encoded characters).

A character in UTF8 can be from 1 to 4 bytes long, subjected to the following rules:

1. For a 1-byte character, the first bit is a 0, followed by its Unicode code.
2. For an n-bytes character, the first n bits are all one's, the n + 1 bit is 0, followed by n - 1 bytes with the most significant 2 bits being 10.

This is how the UTF-8 encoding would work:

     Number of Bytes   |        UTF-8 Octet Sequence
                       |              (binary)
   --------------------+-----------------------------------------
            1          |   0xxxxxxx
            2          |   110xxxxx 10xxxxxx
            3          |   1110xxxx 10xxxxxx 10xxxxxx
            4          |   11110xxx 10xxxxxx 10xxxxxx 10xxxxxx

x denotes a bit in the data that may be either 0 or 1.

Note: The input is an array of integers. Only the least significant 8 bits of each integer is used to store the data. This means each integer represents only 1 byte of data.

Example 1:
Input: data = [197,130,1]
Output: true
Explanation: data represents the octet sequence: 11000101 10000010 00000001.
It is a valid utf-8 encoding for a 2-bytes character followed by a 1-byte character.

Example 2:
Input: data = [235,140,4]
Output: false
Explanation: data represented the octet sequence: 11101011 10001100 00000100.
The first 3 bits are all one's and the 4th bit is 0 refers to one 3-bytes character.
The next byte is a continuation byte which starts with 10 and that's correct.
But the second continuation byte does not start with 10, so it is invalid.

Constraints:
1 <= data.length <= 2 * 10^4
0 <= data[i] <= 255
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem393
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(ValidUtf8([197, 130, 1])); // True
            Console.WriteLine(ValidUtf8([235, 140, 4])); // False
        }

        // Time: O(n) where n is data.Length
        // Space: O(1)
        public static bool ValidUtf8(int[] data)
        {
            if (data == null || data.Length == 0)
            {
                return false;
            }

            int numberOfBytesToProcess = 0;

            foreach (int num in data)
            {
                // Only consider the 8 least significant bits
                int byteVal = num & 0xFF;

                if (numberOfBytesToProcess == 0)
                {
                    if ((byteVal >> 7) == 0)
                    {
                        // 1-byte character (0xxxxxxx)
                        numberOfBytesToProcess = 0;
                    }
                    else if ((byteVal >> 5) == 0b110)
                    {
                        // 2-byte character (110xxxxx) -> 1 continuation byte follows
                        numberOfBytesToProcess = 1;
                    }
                    else if ((byteVal >> 4) == 0b1110)
                    {
                        // 3-byte character (1110xxxx) -> 2 continuation bytes follow
                        numberOfBytesToProcess = 2;
                    }
                    else if ((byteVal >> 3) == 0b11110)
                    {
                        // 4-byte character (11110xxx) -> 3 continuation bytes follow
                        numberOfBytesToProcess = 3;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // Continuation byte must start with 10xxxxxx
                    if ((byteVal >> 6) != 0b10)
                    {
                        return false;
                    }

                    numberOfBytesToProcess--;
                }
            }

            return numberOfBytesToProcess == 0;
        }
    }
}
