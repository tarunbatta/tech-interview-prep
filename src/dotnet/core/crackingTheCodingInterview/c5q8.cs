/*
Draw Line: A monochrome screen is stored as a single array of bytes, allowing eight consecutive
pixels to be stored in one byte. The screen has width w, where w is divisible by 8 (that is, no byte will
be split across rows). The height of the screen, of course, can be derived from the length of the array
and the width. Implement a function that draws a horizontal line from(xl, y) to(x2, y).
The method signature should look something like:
drawLine(byte[] screen, int width, int xl, int x2, int y)
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c5q8 {
    public class Solution {
        public static void Init (string[] args) {
            DrawLine (null, 32, 5, 7, 0);
            DrawLine (new byte[16], 32, 0, 0, 0);
            DrawLine (new byte[16], 32, 7, 5, 0);
            DrawLine (new byte[16], 32, 5, 5, 0);
            DrawLine (new byte[16], 32, 4, 6, 0);
            DrawLine (new byte[16], 32, 5, 25, 0);
        }

        public static void DrawLine (byte[] screen, int width, int x1, int x2, int y) {
            if (x1 > 0 && x2 > 0 && x1 <= x2) {
                int startOffset = x1 % 8;
                int firstByte = startOffset == 0 ? x1 / 8 : (x1 / 8) + 1;
                int endOffset = x2 % 8;
                int lastByte = endOffset == 0 ? x2 / 8 : (x2 / 8) + 1;

                if (screen != null && screen.Length > 0) {
                    // draw line for full bytes
                    for (int i = firstByte; i <= lastByte; ++i) {
                        screen[i + width / 8 * y] = (byte) 0xFF;
                    }

                    // draw start and end of line
                    byte startMask = (byte) (0xFF >> startOffset);
                    byte endMask = (byte) ~(0xFF >> (endOffset + 1));

                    if (x1 / 8 == x2 / 8) {
                        byte mask = (byte) (startMask & endMask);
                        screen[x1 / 8 + width / 8 * y] |= mask;
                    } else {
                        if (startOffset != 0) {
                            screen[firstByte - 1 + width / 8 * y] |= startMask;
                        }
                        if (endOffset != 7) {
                            screen[lastByte + 1 + width / 8 * y] |= endMask;
                        }
                    }

                    PrintScreen (screen, width);
                } else {
                    Console.WriteLine ("Invalid screen.");
                }
            } else {
                Console.WriteLine ("Invalid line parameters.");
            }
        }

        private static void PrintScreen (byte[] screen, int width) {
            int num = 1, widthNum = width / 8;

            foreach (var b in screen) {
                for (int i = 7; i >= 0; --i) {
                    Console.Write ((b >> i) & 1);
                }

                if (num % widthNum == 0) {
                    Console.WriteLine ("");
                }

                ++num;
            }

            Console.WriteLine ("");
        }
    }
}