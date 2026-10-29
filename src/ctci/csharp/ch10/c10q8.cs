/*
Find Duplicates: 

You have an array with all the numbers from 1 to N, where N is at most 32,000. The
array may have duplicate entries and you do not know what N is. With only 4 kilobytes of memory
available, how would you print all duplicate elements in the array?
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c10q8 {
    public class Solution {
        public static List<int> FindDuplicates (int[] arr) {
            List<int> duplicates = new List<int> ();
            BitArray bs = new BitArray (32001);

            foreach (int num in arr) {
                if (bs.Get (num)) {
                    duplicates.Add (num);
                } else {
                    bs.Set (num, true);
                }
            }
            return duplicates;
        }

        public static void Init (string[] args = null) {
            int[] arr = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 8 };
            List<int> dups = FindDuplicates (arr);
            Console.WriteLine ($"Duplicates: {string.Join (", ", dups)}");
        }
    }
}
