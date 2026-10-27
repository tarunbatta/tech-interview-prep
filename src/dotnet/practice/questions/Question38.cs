using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Reverse words in a given string
    /// Program to reverse the order of words not the whole line. "This a good day" should return "day good a This"
    /// </summary>
    public class Question38 {
        public static void Init (string[] args) {
            Console.WriteLine (ReverseOrderOfWords ("This is a good day"));
        }

        private static string ReverseOrderOfWords (string str) {
            string result = string.Empty;

            if (!string.IsNullOrEmpty (str)) {
                char[] arr = str.ToCharArray ();

                int start = 0;
                int end = str.Length - 1;

                for (int i = 0; i < arr.Length; i++) {
                    if (arr[i] == ' ' || i == arr.Length - 1) {
                        end = (i == arr.Length - 1) ? i : i - 1;
                        arr = ReverseWord (arr, start, end);
                        start = i + 1;
                    }
                }

                arr = ReverseWord (arr, 0, arr.Length - 1);
                result = string.Join ("", arr);
            }

            return result;
        }

        private static char[] ReverseWord (char[] arr, int start, int end) {
            while (start < end) {
                char temp = arr[end];
                arr[end] = arr[start];
                arr[start] = temp;

                start++;
                end--;
            }

            return arr;
        }
    }
}