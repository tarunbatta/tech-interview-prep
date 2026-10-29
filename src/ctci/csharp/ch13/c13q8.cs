/*
Lambda Random: 

Using Lambda expressions, write a function List<Integer>
getRandomSubset (List<Integer> list) that returns a random subset of arbitrary size. All
subsets (including the empty set) should be equally likely to be chosen.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.Ctci.c13q8 {
    public class Solution {
        public static List<int> GetRandomSubset (List<int> list) {
            Random random = new Random ();
            return list.Where (_ => random.Next (2) == 0).ToList ();
        }

        public static void Init (string[] args = null) {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> subset = GetRandomSubset (list);
            Console.WriteLine ($"Random subset: [{string.Join (", ", subset)}]");
        }
    }
}
