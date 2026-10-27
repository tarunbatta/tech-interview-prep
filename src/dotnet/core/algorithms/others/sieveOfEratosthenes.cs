/*
Sieve of Eratosthenes
https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    public class SieveOfEratosthenes {
        public static void Init () {
            _SieveOfEratosthenes_a (100);
            _SieveOfEratosthenes_b (100);
        }

        public static void _SieveOfEratosthenes_a (int limit) {
            int[] arr = new int[limit + 1];

            for (int i = 0; i < arr.Length; i++) {
                arr[i] = 1;
            }

            for (int i = 2; i < arr.Length; i++) {
                for (int j = i + 1; j < arr.Length; j++) {
                    if (arr[j] == 1) {
                        if ((j % i) == 0) {
                            arr[j] = 0;
                        }
                    }
                }
            }

            ShowPrimes (arr);
        }

        /// <summary>
        /// more efficient as it removed all multiples
        /// </summary>
        /// <param name="limit"></param>
        public static void _SieveOfEratosthenes_b (int limit) {
            int[] arr = new int[limit + 1];

            for (int i = 0; i < arr.Length; i++) {
                arr[i] = 1;
            }

            for (int i = 2; i < arr.Length; i++) {
                for (int j = i * 2; j < arr.Length; j += i) {
                    if (arr[j] == 1) {
                        if ((j % i) == 0) {
                            arr[j] = 0;
                        }
                    }
                }
            }

            ShowPrimes (arr);
        }

        public static void ShowPrimes (int[] arr) {
            for (int i = 2; i < arr.Length; i++) {
                if (arr[i] == 1) {
                    Console.Write (i + " ");
                }
            }

            Console.WriteLine ("");
        }
    }
}