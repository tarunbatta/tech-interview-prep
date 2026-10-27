using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Consider an array of  integers, where all but one of the integers occur in pairs.
    /// In other words, every element in  occurs exactly twice except for one unique element.
    /// It is guaranteed that array length is an odd number. Find and print the unique element.
    /// </summary>
    public class Question23 {
        public static void Init (string[] args) {
            Console.WriteLine (UniqueNumber (null));
            Console.WriteLine (UniqueNumber (new int[] { 1 }));
            Console.WriteLine (UniqueNumber (new int[] { 1, 1 }));
            Console.WriteLine (UniqueNumber (new int[] { 1, 1, 1 }));
            Console.WriteLine (UniqueNumber (new int[] { 1, 1, 2 }));
            Console.WriteLine (UniqueNumber (new int[] { 1, 2, 2 }));
        }

        private static string UniqueNumber (int[] arr) {
            string result = string.Empty;

            if (arr != null && arr.Length > 0 && arr.Length % 2 != 0) {
                Dictionary<int, int> count = new Dictionary<int, int> ();

                for (int i = 0; i < arr.Length; i++) {
                    if (count.ContainsKey (arr[i])) {
                        count[arr[i]]++;
                    } else {
                        count.Add (arr[i], 1);
                    }
                }

                if (count.ContainsValue (1)) {
                    foreach (var item in count) {
                        if (item.Value == 1) {
                            result = item.Key.ToString ();
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}