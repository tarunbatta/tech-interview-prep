/*
URLify: 

Write a method to replace all spaces in a string with'%20'. You may assume that
the string has sufficient space at the end of the string to hold the additional
characters, and that you are given the "true" length of the string. (Note: Please
use a character array so that you can perform this operation in place.)

EXAMPLE
Input: "Mr John Smith"
Output: "Mr%20Dohn%20Smith"
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c1q3 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine ("Empty: " + ReplaceSpaces_a (""));
            Console.WriteLine ("Null: " + ReplaceSpaces_a (null));
            Console.WriteLine ("Mr John Smith: " + ReplaceSpaces_a ("Mr John Smith"));
            Console.WriteLine (" b : " + ReplaceSpaces_a (" b "));
            Console.WriteLine (" : " + ReplaceSpaces_a (" "));

            Console.WriteLine (" : " + ReplaceSpaces_b (null, 0));
            Console.WriteLine ("Mr John Smith: " + ReplaceSpaces_b ("Mr John Smith    ", 13));
        }

        private static string ReplaceSpaces_a (string input) {
            StringBuilder result = new();

            if (!string.IsNullOrEmpty (input)) {
                for (int i = 0; i < input.Length; i++) {
                    if (input[i] != Char.Parse (" ")) {
                        result.Append (input[i]);
                    } else {
                        result.Append ("%20");
                    }
                }
            }

            return result.ToString ();
        }

        private static string ReplaceSpaces_b (string input, int length) {
            if (string.IsNullOrEmpty (input) || length == 0) {
                return string.Empty;
            }

            char[] result = input.ToCharArray ();
            int counter = result.Length - 1;

            for (int i = length - 1; i >= 0; i--) {
                if (result[i] == ' ') {
                    result[counter] = '0';
                    result[counter - 1] = '2';
                    result[counter - 2] = '%';
                    counter = counter - 3;
                } else {
                    result[counter] = result[i];
                    counter--;
                }
            }

            return new string (result);
        }
    }
}