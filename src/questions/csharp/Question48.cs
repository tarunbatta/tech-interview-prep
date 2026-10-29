using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// You are given two 5 digit numbers (secret and guess), each number has all of its digits unique.
    /// If a digit of secret matches a digit of guess and it appears in the same location in both numbers,
    /// it is considered a “bull”, if the digit matches but is not in the right place it is a “cow”.
    /// Compare the two numbers and determine the number of cows and bulls.
    ///
    /// Example:
    /// Secret, Guess: 12345, 23451 => 0 Bulls, 5 Cows
    /// Secret, Guess: 13579, 12468 => 1 Bulls, 0 Cows
    /// Secret, Guess: 13579, 12345 => 1 Bulls, 2 Cows
    /// Secret, Guess: 13579, 90341 => 0 Bulls, 3 Cows
    /// </summary>
    public class Question48 {
        public static void Init (string[] args) {
            GetBullsAndCows (12345, 23451);
            
            GetBullsAndCows (13579, 12468);
            
            GetBullsAndCows (13579, 12345);
            
            GetBullsAndCows (13579, 90341);
        }

        public static void GetBullsAndCows (int secret, int guess) {
            int bulls = 0;
            int cows = 0;

            char[] s = secret.ToString ().ToCharArray ();
            char[] g = guess.ToString ().ToCharArray ();

            for (int i = 0; i < s.Length; i++) {
                if (s[i] == g[i]) {
                    bulls++;
                } else if (secret.ToString ().Contains (g[i].ToString ())) {
                    cows++;
                }
            }

            Console.WriteLine (secret + ", " + guess + " : " + bulls + " Bulls, " + cows + " Cows");
        }
    }
}