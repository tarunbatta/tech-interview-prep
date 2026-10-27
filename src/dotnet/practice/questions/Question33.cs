using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Rotate Array
    /// Write a function rotate(ar[], d, n) that rotates arr[] of size n by d elements.
    /// 1,2,3,4,5,6,7
    /// Rotation of the above array by 2 will make array
    /// 3,4,5,6,7,1,2
    /// </summary>
    public class Question33 {
        public static void Init (string[] args) {
            RotateArrayByFactor (null, 2);
            
            RotateArrayByFactor (new int[] { 1, 2, 3, 4, 5, 6, 7 }, 0);
            
            RotateArrayByFactor (new int[] { 1, 2, 3, 4, 5, 6, 7 }, 2);
        }

        private static int[] RotateArrayByFactor (int[] arr, int factor) {
            int[] result = null;

            if (arr != null && arr.Length > 0) {
                result = new int[arr.Length];

                if (factor > 0) {
                    int i = 0;
                    int j = i + factor;

                    while (j < result.Length) {
                        result[i] = arr[j];
                        i++;
                        j++;
                    }

                    j = 0;

                    while (i < result.Length) {
                        result[i] = arr[j];
                        i++;
                        j++;
                    }
                } else {
                    result = arr;
                }
            }

            Console.WriteLine (((arr == null) ? "" : string.Join (",", arr)) + " : " + ((result == null) ? "" : string.Join (",", result)));

            return result;
        }
    }
}