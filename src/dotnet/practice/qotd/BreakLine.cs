using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Given a string with spaces, break it into lines, such that, every line is bigger than the previous line.
    /// </summary>
    internal class BreakLine {
        //public static void Main(string[] args)
        //{
        //    LineBreak("a test is simple game");
        //}

        public static void LineBreak (string str) {
            ArrayList arr = new ArrayList ();

            if (!string.IsNullOrEmpty (str)) {
                for (int i = 0; i < str.Length; i++) { }
            }

            foreach (var item in arr) {
                Console.WriteLine (item);
            }
        }
    }
}