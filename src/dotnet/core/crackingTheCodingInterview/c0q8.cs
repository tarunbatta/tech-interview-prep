/*
Write a function to check if the value of a binary number (passed as a string)
equals the hexadecimal representation of a string.
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q8 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (CompareBaseToHex ("00010001", "12"));
            Console.WriteLine (CompareBaseToHex ("00010001", "11"));
            Console.WriteLine (CompareBaseToHex ("01001011", "4B"));
        }

        private static bool CompareBaseToHex (string binary, string hex) {
            bool result = false;

            if (!string.IsNullOrEmpty (binary) && !string.IsNullOrEmpty (hex)) {
                int b = ConvertFromBase (binary, 2);
                int h = ConvertFromBase (hex, 16);

                if (b > 0 && h > 0 && b == h) {
                    result = true;
                }
            }

            return result;
        }

        private static int ConvertFromBase (string num, int baseValue) {
            int result = 0;

            if (baseValue < 2 || (baseValue > 10 && baseValue != 16)) {
                return -1;
            } else {
                for (int i = num.Length - 1; i >= 0; i--) {
                    int digit = Convert.ToInt32 (num[i].ToString (), baseValue);

                    if (digit < 0 || digit >= baseValue) {
                        return -1;
                    } else {
                        int exp = num.Length - i - 1;
                        result += digit * (int) Math.Pow (baseValue, exp);
                    }
                }
            }

            return result;
        }
    }
}