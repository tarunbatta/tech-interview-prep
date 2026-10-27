// Url: https://leetcode.com/problems/read-n-characters-given-read4

/*
157. Read N Characters Given Read4
Easy

Given a file and assume that you can only read the file using a given method read4, implement a method to read n characters.

Method read4:
The API read4 reads four consecutive characters from file, then writes those characters into the buffer array buf4.
The return value is the number of actual characters read.
Note that read4() has its own file pointer, much like FILE *fp in C.

Definition of read4:
    int read4(char[] buf4)

Method read:
By using the read4 method, implement the method read that reads n characters from file and store it in the buffer array buf. Consider that you cannot manipulate file directly.
The return value is the number of actual characters read.

Definition of read:
    int read(char[] buf, int n)

Constraints:
1 <= file.length <= 500
file consist of English letters and digits.
1 <= n <= 1000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem157
{
    public class Reader4
    {
        private static string _content = "leetcode";
        private static int _filePointer = 0;

        public static void SetFileContent(string content)
        {
            _content = content;
            _filePointer = 0;
        }

        public static int Read4(char[] buf4)
        {
            int count = 0;
            while (_filePointer < _content.Length && count < 4)
            {
                buf4[count++] = _content[_filePointer++];
            }
            return count;
        }
    }

    public class Solution : Reader4
    {
        public void Init()
        {
            SetFileContent("leetcode");
            char[] buf = new char[10];
            int readCount = Read(buf, 5);
            Console.WriteLine(new string(buf, 0, readCount)); // leetc
        }

        // Time: O(n) where n is the number of characters to read
        // Space: O(1) auxiliary space (using a 4-byte buffer)
        public static int Read(char[] buf, int n)
        {
            int totalRead = 0;
            char[] buf4 = new char[4];

            while (totalRead < n)
            {
                int count = Read4(buf4);
                int toCopy = Math.Min(count, n - totalRead);

                for (int i = 0; i < toCopy; i++)
                {
                    buf[totalRead++] = buf4[i];
                }

                if (count < 4)
                {
                    break;
                }
            }

            return totalRead;
        }
    }
}