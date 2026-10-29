using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Longest Word:
    /// Given a list of words, find the longest word made of other words in the list.
    /// EXAMPLE
    /// Input: cat, banana, dog, nana, walk, walker, dogwalker
    /// Output: dogwalker
    /// </summary>
    public class LongestWord {
        public static void Init (string[] args) {
            Console.WriteLine (GetLongestWord (null));
            
            Console.WriteLine (GetLongestWord (new string[] { "cat", "banana", "dog", "nana", "walk", "walker", "dogwalker" }));
            
            Console.WriteLine (GetLongestWord (new string[] { "cat", "banana", "dog", "nana", "walk", "walker", "dogwal" }));
        }

        public static string GetLongestWord (string[] words) {
            string result = string.Empty;

            if (words != null && words.Length > 0) {
                Array.Sort (words, (x, y) => y.Length.CompareTo (x.Length));

                for (int i = 0; i < words.Length; i++) {
                    if (isValid (words, words[i], 0)) {
                        result = words[i];
                        break;
                    }
                }
            }

            return result;
        }

        public static bool isValid (string[] words, string word, int index) {
            bool result = false;

            if (!string.IsNullOrEmpty (word) && index < word.Length) {
                for (int i = index + 1; i < word.Length; i++) {
                    string left = word.Substring (0, i);
                    string right = word.Substring (i, word.Length - i);

                    if (IsLeftRightPresent (words, left, right)) {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool IsLeftRightPresent (string[] words, string left, string right) {
            bool isLeft = false;
            bool isRight = false;

            for (int i = 0; i < words.Length; i++) {
                if (isLeft && isRight) {
                    break;
                }

                if (words[i] == left) {
                    isLeft = true;
                } else if (words[i] == right) {
                    isRight = true;
                }
            }

            return isLeft && isRight;
        }
    }
}