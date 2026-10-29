using System;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find duplicates in O(n) time and O(1) extra space
    /// Given an array of n elements which contains elements from 0 to n-1,
    /// with any of these numbers appearing any number of times.
    /// Find these repeating numbers in O(n) and using only constant memory space.
    /// For example, let n be 7 and array be {1, 2, 3, 1, 3, 6, 6}, the answer should be 1, 3 and 6.
    /// </summary>
    public class Question41 {
        public static void Init (string[] args) {
            PrintDuplicates (null);
            
            PrintDuplicates (new int[] { 1 });
            
            PrintDuplicates (new int[] { 1, 1 });
            
            PrintDuplicates (new int[] { 1, 2, 3, 1, 3, 6, 6 });
        }

        public static void PrintDuplicates (int[] nums) {
            if (nums != null && nums.Length > 1) {
                StringBuilder sb = new StringBuilder ();

                for (int i = 0; i < nums.Length; i++) {
                    int abs = nums[Math.Abs (nums[i])];

                    if (abs >= 0) {
                        nums[Math.Abs (nums[i])] = -nums[Math.Abs (nums[i])];
                    } else {
                        if (sb.Length != 0) {
                            sb.Append (",");
                        }

                        sb.Append (Math.Abs (nums[i]));
                    }
                }

                Console.WriteLine (sb.ToString ());
            } else {
                Console.WriteLine ("No duplicates present.");
            }
        }
    }
}