using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to reverse an array of integers
    /// </summary>
    public class Question14 {
        public static void Init (string[] args) {
            Console.WriteLine (ReverseArray (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }));
        }

        private static string ReverseArray (int[] input) {
            string result = string.Empty;

            if (input != null && input.Length > 0) {
                for (int i = 0; i < input.Length / 2; i++) {
                    int temp = input[i];
                    input[i] = input[input.Length - i - 1];
                    input[input.Length - i - 1] = temp;
                }
            }

            return string.Join (" ", input);
        }
    }
}