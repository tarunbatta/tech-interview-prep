using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Given array of integer, find mode of array. Mode is the number that appears most of the time
    /// </summary>
    internal class ModesInArray {
        //public static void Main(string[] args)
        //{
        //    FindModes(new int[] { 1, 2, 3, 1 });
        //    FindModes(new int[] { 1, 1, 2, 2, 3, 3 });
        //}

        public static void FindModes (int[] arr) {
            if (arr != null && arr.Length > 0) {
                Hashtable hash = new Hashtable ();
                int max = 0;

                for (int i = 0; i < arr.Length; i++) {
                    if (hash.Contains (arr[i])) {
                        hash[arr[i]] = (int) hash[arr[i]] + 1;
                    } else {
                        hash.Add (arr[i], 1);
                    }

                    if ((int) hash[arr[i]] > max) {
                        max = (int) hash[arr[i]];
                    }
                }

                foreach (DictionaryEntry item in hash) {
                    if ((int) item.Value == max) {
                        Console.WriteLine (item.Key);
                    }
                }
            }
        }
    }
}