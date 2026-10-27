// Url:https://leetcode.com/problems/integer-to-english-words

/*
273. Integer to English Words
Hard

Convert a non-negative integer to its english words representation. Given input is guaranteed to be less than 231 - 1.

Example 1:

Input: 123
Output: "One Hundred Twenty Three"


Example 2:

Input: 12345
Output: "Twelve Thousand Three Hundred Forty Five"

Example 3:

Input: 1234567
Output: "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"


Example 4:

Input: 1234567891
Output: "One Billion Two Hundred Thirty Four Million Five Hundred Sixty Seven Thousand Eight Hundred Ninety One"
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem273 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        string[] lessThan20 = ["", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"];
        string[] tens = ["", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"];
        string[] thousands = ["", "Thousand", "Million", "Billion"];

        public string NumberToWords (int num) {
            if (num == 0) {
                return "Zero";
            }

            List<string> result = [];
            int count = 0;

            while (num > 0) {
                var temp = getUnderThousandNum (num % 1000);

                if (temp != "") {
                    var thousand = thousands[count];

                    if (thousand != "") {
                        result.Add (thousand);
                    }

                    result.Add (temp);
                }

                num /= 1000;
                count++;
            }

            result.Reverse ();
            return string.Join (" ", result);
        }

        private string getUnderThousandNum (int num) {
            if (num == 0) {
                return "";
            }

            if (num >= 100) {
                var underHundred = getUnderHundredNum (num % 100);

                if (underHundred == "") {
                    return $"{lessThan20[num / 100]} Hundred";
                } else {
                    return $"{lessThan20[num / 100]} Hundred {underHundred}";
                }
            } else {
                return getUnderHundredNum (num);
            }
        }

        private string getUnderHundredNum (int num) {
            if (num < 20) {
                return lessThan20[num];
            } else {
                if (num % 10 == 0) {
                    return $"{tens[num / 10]}";
                } else {
                    return $"{tens[num / 10]} {lessThan20[num % 10]}";
                }
            }
        }
    }
}