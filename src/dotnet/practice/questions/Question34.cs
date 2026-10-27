using System;
using System.Collections;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Count pairs with given sum:
    /// Given an array of integers, and a number ‘sum’, find the number of pairs of integers in the array whose sum is equal to ‘sum’.
    /// Examples:
    /// Input  :  arr[] = {1, 5, 7, -1}, sum = 6
    /// Output :  2
    /// Pairs with sum 6 are(1, 5) and(7, -1)
    ///
    /// Input  :  arr[] = {1, 5, 7, -1, 5}, sum = 6
    /// Output :  3
    /// Pairs with sum 6 are(1, 5), (7, -1) & (1, 5)
    ///
    /// Input  :  arr[] = {1, 1, 1, 1}, sum = 2
    /// Output :  6
    /// There are 3! pairs with sum 2.
    ///
    /// Input  :  arr[] = {10, 12, 10, 15, -1, 7, 6, 5, 4, 2, 1, 1, 1}, sum = 11
    /// Output :  9
    /// </summary>
    public class Question34 {
        public static void Init (string[] args) {
            //GetArrayPairSum(null, 0);
            //Program.PrintLine();
            CountPairWithSum (new int[] { 1, 5, 7, -1 }, 6);
            
            CountPairWithSum (new int[] { 1, 5, 7, -1, 5 }, 6);
            
            CountPairWithSum (new int[] { 1, 1, 1, 1 }, 2);
            
            CountPairWithSum (new int[] { 10, 12, 10, 15, -1, 7, 6, 5, 4, 2, 1, 1, 1 }, 11);
        }

        private static int CountPairWithSum (int[] arr, int sum) {
            int result = 0;

            if (arr != null && arr.Length > 1) {
                Hashtable hash = new Hashtable ();

                for (int i = 0; i < arr.Length; i++) {
                    if (!hash.ContainsKey (arr[i])) {
                        hash.Add (arr[i], 1);
                    } else {
                        hash[arr[i]] = (int) hash[arr[i]] + 1;
                    }
                }

                for (int i = 0; i < arr.Length; i++) {
                    int target = sum - arr[i];

                    if (hash.ContainsKey (target)) {
                        result += (int) hash[target];
                    }

                    if (target == arr[i]) {
                        result--;
                    }
                }
            }

            result = result / 2;

            Console.WriteLine ("'" + (arr == null ? "" : string.Join (",", arr)) + "' with " + sum + " has " + result + " pairs of integers.");

            return result;
        }
    }
}