/*
String Rotation: Assume you have a method isSubstring which checks if one word is a substring of another.
Given two strings, s1 and s2,
Write code to check if s2 is a rotation of s1 using only one call to isSubstring
(e.g.,"waterbottle"is a rotation of "erbottlewat").
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c1q9 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (isRotation ("erbottlewat", "waterbottle"));
            Console.WriteLine (isRotation ("erbottlewat", "waterbottlz"));
        }

        private static bool isRotation (string s1, string s2) {
            bool result = false;

            if (!string.IsNullOrEmpty (s1) && !string.IsNullOrEmpty (s2)) {
                result = isSubstring (s1 + s1, s2) >= 0;
            }

            return result;
        }

        private static int isSubstring (string s1, string s2) {
            int position = -1;

            if (!string.IsNullOrEmpty (s1) && !string.IsNullOrEmpty (s2)) {
                for (int i = 0; i < s1.Length - s2.Length; i++) {
                    int counter = 0;

                    for (int j = 0; j < s2.Length; j++) {
                        if (s1[i] != s2[j]) {
                            break;
                        } else {
                            i++;
                            counter++;
                        }

                        if (counter == s2.Length) {
                            position = i - s2.Length;
                        }
                    }
                }
            }

            return position;
        }
    }
}