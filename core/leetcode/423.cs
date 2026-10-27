using System.Text;
// Url:https://leetcode.com/problems/reconstruct-original-digits-from-english

/*
423. Reconstruct Original Digits from English
Medium

Given a non-empty string containing an out-of-order English representation of digits 0-9, output the digits in ascending order.

Note:
Input contains only lowercase English letters.
Input is guaranteed to be valid and can be transformed to its original digits. That means invalid inputs such as "abc" or "zerone" are not permitted.
Input length is less than 50,000.

Example 1:
Input: "owoztneoer"
Output: "012"

Example 2:
Input: "fviefuro"
Output: "45"
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem423 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (OriginalDigits_a("owoztneoer"));
            Console.WriteLine (OriginalDigits_a("fviefuro"));
            Console.WriteLine (OriginalDigits_a("zeroonetwothreefourfivesixseveneightnine"));

            Console.WriteLine (OriginalDigits_b("owoztneoer"));
            Console.WriteLine (OriginalDigits_b("fviefuro"));
            Console.WriteLine (OriginalDigits_b("zeroonetwothreefourfivesixseveneightnine"));
        }

        public static string OriginalDigits_a (string s) {
            string result = string.Empty;

            string[] word = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

            int[] count = new int[26];
            foreach (char c in s) {
                count[c - 'a']++;
            }

            int[] res = new int[10];
            res[0] = count['z' - 'a'];
            res[2] = count['w' - 'a'];
            res[4] = count['u' - 'a'];
            res[6] = count['x' - 'a'];
            res[8] = count['g' - 'a'];

            int i = 0;
            while (i <= 8) {
                foreach (char item in word[i]) {
                    count[item - 'a'] -= res[i];
                }
                i += 2;
            }

            res[1] = count['o' - 'a'];
            res[3] = count['t' - 'a'];
            res[5] = count['f' - 'a'];
            res[7] = count['s' - 'a'];

            i = 1;
            while (i <= 7) {
                foreach (char item in word[i]) {
                    count[item - 'a'] -= res[i];
                }
                i += 2;
            }

            res[9] = count['i' - 'a'];

            StringBuilder sb = new StringBuilder ();

            for (i = 0; i < 10; i++) {
                for (int j = 0; j < res[i]; j++) {
                    sb.Append (i);
                }
            }

            result = sb.ToString ();

            return result;
        }

        public static string OriginalDigits_b (string s) {
            char[] sc = s.ToCharArray ();
            int[] count = new int[10];

            foreach (char c in sc) {
                switch (c) {
                    case 'z':
                        count[0]++;
                        break; // zero
                    case 'w':
                        count[2]++;
                        break; // two
                    case 'u':
                        count[4]++;
                        break; // four
                    case 'x':
                        count[6]++;
                        break; // six
                    case 'g':
                        count[8]++;
                        break; // eight
                    case 's':
                        count[7]++;
                        break; // = (six, seven) 
                    case 'f':
                        count[5]++;
                        break; // = (four, five)
                    case 'o':
                        count[1]++;
                        break; // = (zero, one, two, four)
                    case 't':
                        count[3]++;
                        break; // = (two, three, eight)
                    case 'i':
                        count[9]++;
                        break; // = (five, six, eight, nine) 
                }
            }

            count[7] = count[7] - count[6];
            count[5] = count[5] - count[4];
            count[1] = count[1] - count[0] - count[2] - count[4];
            count[3] = count[3] - count[2] - count[8];
            count[9] = count[9] - count[5] - count[6] - count[8];

            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < count[i]; j++) {
                    sb.Append (i);
                }
            }

            return sb.ToString ();
        }
    }
}