/*
Palindrome: 

Implement a function to check if a linked list is a palindrome.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q6 {
    public class Solution {
        public class Node {
            public Node next;
            public char val;

            public Node (char val) {
                this.val = val;
            }

            public override string ToString () {
                StringBuilder sb = new StringBuilder ();
                Node temp = this;
                while (temp != null) {
                    sb.Append (temp.val);
                    temp = temp.next;
                }
                return sb.ToString ();
            }
        }

        public static void Init (string[] args = null) {
            Node a = new Node ('r') { next = new Node ('a') { next = new Node ('r') } };
            Node b = new Node ('a') { next = new Node ('b') };

            Console.WriteLine ($"rar is palindrome: {IsPalindrome (a)}"); // True
            Console.WriteLine ($"ab is palindrome: {IsPalindrome (b)}");  // False
        }

        public static bool IsPalindrome (Node node) {
            Stack<char> stack = new Stack<char> ();
            Node temp = node;
            while (temp != null) {
                stack.Push (temp.val);
                temp = temp.next;
            }
            temp = node;
            while (temp != null) {
                char top = stack.Pop ();
                if (temp.val != top) {
                    return false;
                }
                temp = temp.next;
            }
            return true;
        }
    }
}
