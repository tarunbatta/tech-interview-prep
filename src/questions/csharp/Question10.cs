using System;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Rotate/shift the elements of a square array clockwise by one at a time. (Not by 90 degrees, but by one element. Think of concentric circles.)
    /// Input will be of the following format to be read in:
    /// i. The first line of input will be a number indicating how many rows are in the matrix.
    /// ii. This will be followed by lines of input, one for each row of the matrix.
    /// iii. Each row will contain the same number of columns with each column separated by a space.
    /// Output will be the matrix written with each row on a separate line and columns separated by spaces.
    /// Print ERROR if the input matrix is not square.
    /// </summary>
    public class Question10 {
        public static void Init (string[] args) {
            Console.WriteLine (RotateShiftArray (2, "1 2 3 4"));
            Console.WriteLine (RotateShiftArray (3, "1 2 3 4 5 6 7 8 9"));
            Console.WriteLine (RotateShiftArray (2, "1 2 3 4 5 6"));
        }

        private static string RotateShiftArray (int matrixLength, string matrix) {
            string result = string.Empty;

            if (matrixLength > 0 && !string.IsNullOrEmpty (matrix) && (matrix.Length + 1) == Math.Pow (matrixLength, 2) * 2) {
                int[, ] arr = PopulateArray (matrixLength, matrix);

                for (int i = 0; i < matrixLength / 2; i++) {
                    for (int j = 0; j < (matrixLength + 1) / 2; j++) {
                        int temp = arr[i, j];
                        arr[i, j] = arr[matrixLength - 1 - j, i];
                        arr[matrixLength - 1 - j, i] = arr[matrixLength - 1 - i, matrixLength - 1 - j];
                        arr[matrixLength - 1 - i, matrixLength - 1 - j] = arr[j, matrixLength - 1 - i];
                        arr[j, matrixLength - 1 - i] = temp;
                    }
                }

                result = DisplayArray (matrixLength, arr);
            } else {
                result = "ERROR";
            }

            return result;
        }

        private static int[, ] PopulateArray (int length, string values) {
            int[, ] result = new int[length, length];

            if (length > 0 && !string.IsNullOrEmpty (values)) {
                int x = 0;
                int y = 0;
                var valuesArray = values.Split (' ');

                for (int i = 0; i < valuesArray.Length; i++) {
                    result[x, y] = Convert.ToInt32 (valuesArray[i]);
                    y++;

                    if (y == length) {
                        x++;
                        y = 0;
                    }
                }
            }

            return result;
        }

        private static string DisplayArray (int length, int[, ] arr) {
            StringBuilder result = new StringBuilder ();

            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    result.Append (arr[i, j]);
                }
            }

            return result.ToString ();
        }
    }
}