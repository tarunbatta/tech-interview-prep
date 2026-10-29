using System;
using System.Linq;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// The Next Palindrome
    /// A positive integer is called a palindrome if its representation in the decimal system is the same
    /// when read from left to right and from right to left. For a given positive integer K of not more
    /// than 1000000 digits, write the value of the smallest palindrome larger than K to output.
    /// Numbers are always displayed without leading zeros.
    ///
    /// Example:
    /// Input:  2133
    /// Output: 2222
    /// </summary>
    public class NextPalindrome {
        public static void Init (string[] args) {
            Console.WriteLine (GetNextPalindrome (null));
            
            Console.WriteLine (GetNextPalindrome ("0"));
            
            Console.WriteLine (GetNextPalindrome ("5"));
            
            Console.WriteLine (GetNextPalindrome ("9"));
            
            Console.WriteLine (GetNextPalindrome ("999"));
            
            Console.WriteLine (GetNextPalindrome ("1234"));
            
            Console.WriteLine (GetNextPalindrome ("2133"));
            
            Console.WriteLine (GetNextPalindrome ("1234628"));
        }

        public static int GetNextPalindrome (string str) {
            int result = 0;

            if (!string.IsNullOrEmpty (str)) {
                int num = Convert.ToInt32 (str);
                int[] arr = str.Select (x => x - '0').ToArray ();

                if (num < 0) {
                    result = 0;
                } else if (num < 9) {
                    result = num + 1;
                } else if (All9s (arr)) {
                    int[] next = new int[arr.Length + 1];
                    next[0] = 1;
                    next[arr.Length] = 1;

                    for (int i = 1; i < arr.Length; i++) {
                        next[i] = 0;
                    }

                    result = ConvertArrayToInt (next);
                } else {
                    result = ConvertArrayToInt (OtherCases (arr));
                }
            }

            return result;
        }

        public static int[] OtherCases (int[] arr) {
            int mid = arr.Length / 2;
            int i = mid - 1;
            int j = arr.Length % 2 == 0 ? mid : mid + 1;

            // ignore the same digits from the middle
            while (i >= 0 && arr[i] == arr[j]) {
                i--;
                j++;
            }

            // A bool variable to check if copy of left side to right is sufficient or not
            bool leftsmaller = false;

            // Find if the middle digit(s) need to be incremented or not (or copying left side is not sufficient)
            if (i < 0 || arr[i] < arr[j]) {
                leftsmaller = true;
            }

            // Copy the mirror of left to tight
            while (i >= 0) {
                arr[j] = arr[i];
                j++;
                i--;
            }

            // Handle the case where middle digit(s) must be incremented.
            // This part of code is for CASE 1 and CASE 2.2
            if (leftsmaller == true) {
                int carry = 1;
                i = mid - 1;

                // If there are odd digits, then increment
                // the middle digit and store the carry
                if (arr.Length % 2 == 1) {
                    arr[mid] += carry;
                    carry = arr[mid] / 10;
                    arr[mid] %= 10;
                    j = mid + 1;
                } else
                    j = mid;

                // Add 1 to the rightmost digit of the left side, propagate the carry
                // towards MSB digit and simultaneously copying mirror of the left side
                // to the right side.
                while (i >= 0) {
                    arr[i] += carry;
                    carry = arr[i] / 10;
                    arr[i] %= 10;
                    arr[j++] = arr[i--]; // copy mirror to right
                }
            }

            return arr;
        }

        public static bool All9s (int[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] != 9) {
                    return false;
                }
            }

            return true;
        }

        public static int ConvertArrayToInt (int[] arr) {
            return Convert.ToInt32 (String.Join ("", arr.Select (p => p.ToString ()).ToArray ()));
        }
    }
}