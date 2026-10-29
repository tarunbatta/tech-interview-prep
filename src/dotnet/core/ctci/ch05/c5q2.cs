/*
Binary to String: 

Given a real number between 0 and 1 (e.g., 0.72) that is passed in as a double, print
the binary representation. If the number cannot be represented accurately in binary with at most 32
characters, print "ERROR".
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c5q2 {
    public class Solution {
        public static string BinaryToString (double x) {
            if (x >= 1 || x <= 0) {
                return "ERROR";
            }

            StringBuilder sb = new StringBuilder ();
            sb.Append (".");
            while (x > 0) {
                if (sb.Length > 32) {
                    return "ERROR";
                }

                double r = x * 2;
                if (r >= 1) {
                    sb.Append (1);
                    x = r - 1;
                } else {
                    sb.Append (0);
                    x = r;
                }
            }
            return sb.ToString ();
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"0.72: {BinaryToString (0.72)}");
            Console.WriteLine ($"0.625: {BinaryToString (0.625)}"); // .101
        }
    }
}
