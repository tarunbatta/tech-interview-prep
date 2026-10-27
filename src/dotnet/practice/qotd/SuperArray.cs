using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Building a Super Array
    /// Can you build a stack like data structure that stores an integer and supports the following operations.
    /// a) Push
    /// b) Pop
    /// c) Peek
    /// d) Find Average of all elements in the array
    /// e) Find Maximum value in the Stack
    /// f) Find the Median of the value in the Stack
    /// </summary>
    public class SuperArray {
        private int[] data;
        private int capacity;
        private int pointer;

        public SuperArray (int length) {
            this.pointer = -1;
            this.capacity = length;
            this.data = new int[this.capacity];
        }

        public void Push (int val) {
            if (this.data != null && this.data.Length == this.capacity) {
                throw new OverflowException ();
            } else {
                this.pointer++;
                this.data[this.pointer] = val;
            }
        }

        public int Pop () {
            return 0;
        }

        public int Peek () {
            return 0;
        }

        public int Average () {
            return 0;
        }

        public int Max () {
            return 0;
        }

        public int Median () {
            return 0;
        }
    }
}