using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Given an array of numbers return the list of Prime Numbers
    /// </summary>
    public class Question28

    {
        public static void Init (string[] args) {
            int[] arr = new int[1000];

            for (int i = 0; i < arr.Length; i++) {
                arr[i] = i - (arr.Length / 2);
            }

            Console.WriteLine (GetPrimeNumbers (arr));
        }

        private static string GetPrimeNumbers (int[] n) {
            StringBuilder result = new StringBuilder ();

            if (n != null && n.Length > 0) {
                Dictionary<int, bool> cache = new Dictionary<int, bool> ();

                for (int i = 0; i < n.Length; i++) {
                    if (IsPrime (cache, n[i])) {
                        result.Append (n[i] + ",");
                    }
                }
            }

            return result.ToString ();
        }

        private static bool IsPrime (Dictionary<int, bool> cache, int n) {
            bool result = true;

            if (cache.ContainsKey (n)) {
                result = cache[n];
            } else {
                if (n < 1) {
                    result = false;
                    cache.Add (n, result);
                } else if (n == 1) {
                    result = true;
                    cache.Add (n, result);
                } else {
                    for (int i = 2; i * i <= n; i++) {
                        if (n % i == 0) {
                            result = false;
                            cache.Add (n, result);
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}