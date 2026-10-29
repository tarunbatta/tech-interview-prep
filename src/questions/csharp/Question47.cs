using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Even numbers at even index and Odd numbers at odd index
    /// Given an array of size 2n, with n od dnumbers and n even numbers.
    /// Rearrange the array in place so that all odd numbers are in odd numbered locations.
    /// </summary>
    public class Question47 {
        public static void Init (string[] args) {
            RearrangeEvenOdd (null);
            
            RearrangeEvenOdd (new int[] { 2, 5, 7, 8, 1, 6, 9, 0 });
        }

        public static int[] RearrangeEvenOdd (int[] arr) {
            int[] result = null;

            if (arr != null && arr.Length > 0) {
                result = new int[arr.Length];

                for (int i = 0; i < arr.Length; i++) {
                    result[i] = arr[i];
                }

                int even = 0;
                int odd = 1;

                while (true) {
                    while (even < result.Length && result[even] % 2 == 0) {
                        even += 2;
                    }

                    while (odd < result.Length && result[odd] % 2 == 1) {
                        odd += 2;
                    }

                    if (even < result.Length && odd < result.Length) {
                        int temp = result[odd];
                        result[odd] = result[even];
                        result[even] = temp;
                    } else {
                        break;
                    }
                }
            }

            Console.WriteLine ((arr == null ? string.Empty : string.Join (",", arr)) + " : " + (result == null ? string.Empty : string.Join (",", result)));

            return result;
        }
    }
}