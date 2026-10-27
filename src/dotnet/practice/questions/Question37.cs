using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Perform runtime encoding of string in place.
    /// Example:
    /// Input: aaaabbcccd
    /// Output: 4a2b3cd
    /// Input: aaabcdef
    /// Output: 3abcdef
    /// Input: aabcdef
    /// Output: aabcdef
    /// In addition, write a function for reversal as well.
    /// </summary>
    public class Question37 {
        public static void Init (string[] args) {
            Encode (null);
            
            Encode (("aaaabbcccd").ToCharArray ());
            
            Encode (("aaabcdef").ToCharArray ());
            
            Encode (("aabcdef").ToCharArray ());
            
            Encode (("abcdef").ToCharArray ());
        }

        private static char[] Encode (char[] arr) {
            string input = (arr == null) ? "" : string.Join ("", arr);

            if (arr != null && arr.Length > 0) {
                int start = 0;
                int i = 1;
                int count = 1;
                char current = arr[start];

                while (i < arr.Length) {
                    if (current == arr[i]) {
                        count++;
                    } else {
                        if (count > 2) {
                            arr[start] = (char) ('0' + count);
                            arr[start + 1] = current;

                            start = start + 2;
                        } else {
                            for (int x = 0; x < count; x++) {
                                arr[start] = current;
                                start++;
                            }
                        }

                        count = 1;
                        current = arr[i];
                    }

                    i++;

                    if (i == arr.Length) {
                        arr[start] = current;
                        start++;
                    }
                }

                for (int j = start; j < arr.Length; j++) {
                    arr[j] = ' ';
                }
            }

            string output = (arr == null) ? "" : string.Join ("", arr);
            Console.WriteLine ("'" + input + "' : '" + output + "'");

            return arr;
        }
    }
}