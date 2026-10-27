using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Position of rightmost set bit
    /// Write a function to return position of first 1 from right to left, in binary representation of an Integer.
    /// I/P    18,   Binary Representation 010010
    /// O/P    2
    /// I/P    19,   Binary Representation 010011
    /// O/P    1
    /// I/P    19,   Binary Representation 1100
    /// O/P    3
    /// </summary>
    public class Question26 {
        public static void Init (string[] args) {
            Console.WriteLine ("18: " + GetRightMostBit (18));
            Console.WriteLine ("19: " + GetRightMostBit (19));
            Console.WriteLine ("12: " + GetRightMostBit (12));
        }

        /// <summary>
        /// Algorithm: (Example 18(010010))
        /// 1. Take two's complement of the given no as all bits are reverted except the first '1' from right to left(10111)
        /// 2  Do an bit-wise & with original no, this will return no with the required one only (00010)
        /// 3  Take the log2 of the no, you will get position - 1(1)
        /// 4  Add 1(2)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int GetRightMostBit (int n) {
            return Convert.ToInt32 (Math.Log (n & -n, 2)) + 1;
        }
    }
}