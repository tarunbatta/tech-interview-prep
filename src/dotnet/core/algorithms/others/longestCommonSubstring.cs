/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    public class LongestCommonSubstring {
        public static void Init () {
            Console.WriteLine (LCS ("rosesarered", "are"));
            Console.WriteLine (LCSRecursive ("rosesarered", "are"));
        }

        public static int LCS (string x, string y) {
            int[, ] LCStuff = new int[x.Length + 1, y.Length + 1];
            int result = 0;

            for (int i = 0; i <= x.Length; i++) {
                for (int j = 0; j <= y.Length; j++) {
                    if (i == 0 || j == 0)
                        LCStuff[i, j] = 0;
                    else if (x[i - 1] == y[j - 1]) {
                        LCStuff[i, j] = LCStuff[i - 1, j - 1] + 1;
                        result = Math.Max (result, LCStuff[i, j]);
                    } else
                        LCStuff[i, j] = 0;
                }
            }

            return result;
        }

        public static int LCSRecursive (string x, string y) {
            if (string.IsNullOrEmpty (x) || string.IsNullOrEmpty (y)) {
                return 0;
            }

            return LCSRecursiveHelper (x, y, x.Length - 1, y.Length - 1);
        }

        public static int LCSRecursiveHelper (string x, string y, int i, int j) {
            if (i < 0 || j < 0) {
                return 0;
            }

            if (x[i] == y[j]) {
                return 1 + LCSRecursiveHelper (x, y, i - 1, j - 1);
            } else {
                return Math.Max (LCSRecursiveHelper (x, y, i - 1, j), LCSRecursiveHelper (x, y, i, j - 1));
            }
        }
    }
}