using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function that determines whether a two-dimensional integer array contains duplicate values within k indices of each other (manhattan distance).
    /// </summary>
    public class Question11 {
        public static void Init (string[] args) {
            Console.WriteLine (CheckDuplicates (4, "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16", 3));
            Console.WriteLine (CheckDuplicates (3, "1 2 3 4 5 6 5 8 9", 2));
            Console.WriteLine (CheckDuplicates (4, "1 2 3 4 5 6 7 8 2 10 11 12", 2));
        }

        private static string CheckDuplicates (int matrixlength, string matrix, int distance) {
            if (distance > 0) {
                var matrixarray = PopulateArray (matrixlength, matrix);

                for (int i = 0; i < matrixlength; i++) {
                    for (int j = 0; j < matrixlength; j++) {
                        if ((j + distance) < matrixlength && matrixarray[i, j] == matrixarray[i, j + distance]) {
                            return "yes";
                        } else if ((j + distance) >= matrixlength) {
                            int x = i + 1;
                            int y = ((j + distance) / matrixlength) - 1;

                            if (x < matrixlength && matrixarray[i, j] == matrixarray[x, y]) {
                                return "yes";
                            }
                        }
                    }
                }
            }

            return "no";
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
    }
}