using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Bleeding Bits
    /// Given an integer, write a function to bleed the 1's on both sides by a given factor
    /// </summary>
    public class Question46 {
        public static void Init (string[] args) {
            BleedBits (0, 1);
            
            BleedBits (72, 0);
            
            BleedBits (72, 1);
            
            BleedBits (66052, 2);
        }

        public static int BleedBits (int num, int factor) {
            int result = num;

            if (factor > 0) {
                int left = num;

                for (int i = 0; i < factor; i++) {
                    left = left | (left << 1);
                }

                int right = num;

                for (int i = 0; i < factor; i++) {
                    right = right | (right >> 1);
                }

                result = num | left | right;
            }

            Console.WriteLine (num + " (" + Convert.ToString (num, 2) + ") : " + Convert.ToString (result, 2));

            return result;
        }
    }
}