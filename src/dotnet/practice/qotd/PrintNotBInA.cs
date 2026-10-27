using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Given two int array, print all elements of A that do not appear in B. In case of duplicates, print the extra repetitions of A which are not in B.
    /// </summary>
    internal class PrintNotBInA {
        //public static void Main(string[] args)
        //{
        //    PrintAllElementsOfAThatCannotBeMatchedInB(null, null);
        //    PrintAllElementsOfAThatCannotBeMatchedInB(new int[] {1}, null);
        //    PrintAllElementsOfAThatCannotBeMatchedInB(null, new int[] {1});
        //    PrintAllElementsOfAThatCannotBeMatchedInB(new int[] {1, 2, 3, 4}, new int[] {2, 4});
        //    PrintAllElementsOfAThatCannotBeMatchedInB(new int[] {1, 2, 3, 3, 4, 4}, new int[] {2, 4});
        //}

        public static void PrintAllElementsOfAThatCannotBeMatchedInB (int[] a, int[] b) {
            string result = String.Empty;
            Hashtable hash = new Hashtable ();

            if (b != null && b.Length > 0) {
                for (int i = 0; i < b.Length; i++) {
                    if (hash.Contains (b[i])) {
                        hash[b[i]] = (int) hash[b[i]] + 1;
                    } else {
                        hash.Add (b[i], 1);
                    }
                }
            }

            if (a != null && a.Length > 0) {
                for (int i = 0; i < a.Length; i++) {
                    if (hash.Contains (a[i]) && (int) hash[a[i]] > 0) {
                        hash[a[i]] = (int) hash[a[i]] - 1;
                    } else {
                        result += a[i] + " ";
                    }
                }
            }

            Console.WriteLine (result);
        }
    }
}