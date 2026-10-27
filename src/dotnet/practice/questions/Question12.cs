using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find the unique common elements from two given lists of ordered and unique numbers.
    /// </summary>
    public class Question12 {
        public static void Init (string[] args) {
            Console.WriteLine (Intersect_a (null, null));
            
            Console.WriteLine (Intersect_a (new List<int> () { 1, 2, 3, 4 }, null));
            
            Console.WriteLine (Intersect_a (null, new List<int> () { 2, 4 }));
            
            Console.WriteLine (Intersect_a (new List<int> () { 1, 2, 3, 4 }, new List<int> () { 2, 4 }));

            

            Console.WriteLine (Intersect_b (null, null));
            
            Console.WriteLine (Intersect_b (new List<int> () { 1, 2, 3, 4 }, null));
            
            Console.WriteLine (Intersect_b (null, new List<int> () { 2, 4 }));
            
            Console.WriteLine (Intersect_b (new List<int> () { 1, 2, 3, 4 }, new List<int> () { 2, 4 }));
        }

        private static string Intersect_a (List<int> a, List<int> b) {
            StringBuilder result = new StringBuilder ();

            if (a != null && b != null && a.Count > 0 && b.Count > 0) {
                for (int i = 0; i < a.Count; i++) {
                    for (int j = 0; j < b.Count; j++) {
                        if (a[i] == b[j]) {
                            result.Append (a[i] + " ");
                        } else if (a[i] < b[j]) {
                            break;
                        }
                    }
                }
            }

            return result.ToString ().Trim ();
        }

        private static string Intersect_b (List<int> a, List<int> b) {
            StringBuilder result = new StringBuilder ();

            if (a != null && b != null && a.Count > 0 && b.Count > 0) {
                int i = 0;
                int j = 0;

                while (i < a.Count && j < b.Count) {
                    if (a[i] == b[j]) {
                        result.Append (a[i] + " ");
                        i++;
                        j++;
                    } else if (a[i] < b[j]) {
                        i++;
                    } else {
                        j++;
                    }
                }
            }

            return result.ToString ().Trim ();
        }
    }
}