/*
Sort Stack: 

Write a program to sort a stack such that the smallest items are on the top. You can use
an additional temporary stack, but you may not copy the elements into any other data structure
(such as an array). The stack supports the following operations: push, pop, peek, and is Empty.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c3q5 {
    public class Solution {
        public static Stack<int> Sort (Stack<int> s) {
            Stack<int> r = new Stack<int> ();
            while (s.Count > 0) {
                int tmp = s.Pop ();
                while (r.Count > 0 && r.Peek () > tmp) {
                    s.Push (r.Pop ());
                }
                r.Push (tmp);
            }
            return r;
        }

        public static void Init (string[] args = null) {
            Stack<int> s = new Stack<int> ();
            s.Push (3);
            s.Push (1);
            s.Push (4);
            s.Push (2);

            Stack<int> sorted = Sort (s);
            Console.Write ("Sorted Stack (top to bottom): ");
            while (sorted.Count > 0) {
                Console.Write (sorted.Pop () + " ");
            }
            Console.WriteLine ();
        }
    }
}
