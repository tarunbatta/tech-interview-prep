// Url: https://leetcode.com/problems/read-n-characters-given-read4-ii-call-multiple-times

/*
158. Read N Characters Given Read4 II - Call multiple times
Hard

Given a file and assume that you can only read the file using a given method read4, implement a method read to read n characters. Your read method may be called multiple times.

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
All the calls to read are valid.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem158
{
    public class Reader4
    {
        private static string _content = "abcde";
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
        private readonly char[] _internalBuffer = new char[4];
        private int _bufferHead = 0;
        private int _bufferTail = 0;

        public void Init()
        {
            SetFileContent("abcde");
            char[] buf = new char[10];

            int read1 = Read(buf, 1);
            Console.WriteLine(new string(buf, 0, read1)); // a

            int read2 = Read(buf, 2);
            Console.WriteLine(new string(buf, 0, read2)); // bc

            int read3 = Read(buf, 3);
            Console.WriteLine(new string(buf, 0, read3)); // de
        }

        // Time: O(n) where n is the number of characters to read across calls
        // Space: O(1) auxiliary space (4-byte internal state buffer)
        public int Read(char[] buf, int n)
        {
            int totalRead = 0;

            while (totalRead < n)
            {
                if (_bufferHead == _bufferTail)
                {
                    _bufferHead = 0;
                    _bufferTail = Read4(_internalBuffer);

                    if (_bufferTail == 0)
                    {
                        break;
                    }
                }

                while (totalRead < n && _bufferHead < _bufferTail)
                {
                    buf[totalRead++] = _internalBuffer[_bufferHead++];
                }
            }

            return totalRead;
        }
    }
}