// Url:https://leetcode.com/problems/fizz-buzz

/*
412. Fizz Buzz
Easy

Write a program that outputs the string representation of numbers from 1 to n.

But for multiples of three it should output “Fizz” instead of the number and for the multiples of five output “Buzz”. For numbers which are multiples of both three and five output “FizzBuzz”.

Example:
n = 15,

Return:
[
    "1",
    "2",
    "Fizz",
    "4",
    "Buzz",
    "Fizz",
    "7",
    "8",
    "Fizz",
    "Buzz",
    "11",
    "Fizz",
    "13",
    "14",
    "FizzBuzz"
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem412 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (FizzBuzz(0));
            Console.WriteLine (FizzBuzz(15));
        }

        public static IList<string> FizzBuzz (int n) {
            List<string> result = [];

            if (n > 0) {
                for (int i = 1; i <= n; i++) {
                    string temp = string.Empty;

                    if (i % 3 == 0) {
                        temp += "Fizz";
                    }

                    if (i % 5 == 0) {
                        temp += "Buzz";
                    }

                    if (string.IsNullOrEmpty (temp)) {
                        temp = i.ToString ();
                    }

                    result.Add (temp);
                }
            }

            return result;
        }
    }
}