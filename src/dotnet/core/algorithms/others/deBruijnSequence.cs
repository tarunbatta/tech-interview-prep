using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    /// <summary>
    /// De Bruijn Sequence (https://en.wikipedia.org/wiki/De_Bruijn_sequence)
    ///
    /// Print out a de Bruijn sequence of order n. This is a shortest
    /// (circular) string such that every sequence of n bits appears
    /// as a substring at least once.
    ///
    /// Algorithm: start with n 0's. Append a 1 if the n-tuple that
    /// would be formed has not already appeared in the sequence;
    /// append a 0 otherwise.
    ///
    /// Note: not the most space or time efficient algorithm.
    /// DeBruijn 4
    /// 0000111101100101
    ///
    /// DeBruijn 5
    /// 00000111110111001101011000101001
    ///
    /// DeBruijn 6
    /// 0000001111110111100111010111000110110100110010110000101010001001
    /// </summary>
    public class DeBruijnSequence {
        public static void Init (string[] args) {
            BuildDeBruijnSequence (3);
            
            BuildDeBruijnSequence (4);
            
            BuildDeBruijnSequence (5);
            
            BuildDeBruijnSequence (6);
        }

        private static string BuildDeBruijnSequence (int n) {
            StringBuilder result = new StringBuilder ();

            for (int i = 0; i < n; i++) {
                result.Append ("0");
            }

            int counter = 1;

            while (counter < (1 << n)) {
                string current = Convert.ToString (counter, 2);

                if (result.ToString ().IndexOf (current) > 0) {
                    result.Append ("0");
                } else {
                    result.Append ("1");
                }

                counter++;
            }

            Console.WriteLine ("De Bruijn Sequence of order " + n + " is : " + result.ToString () + " (" + result.ToString ().Length + ")");

            return result.ToString ();
        }
    }
}