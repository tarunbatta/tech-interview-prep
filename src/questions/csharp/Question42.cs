using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Create a Max Stack DataStructure which has Add, Remove and CurrentMax methods with O(1) time complexity
    /// </summary>
    public class Question42 {
        public static void Init (string[] args) {
            MaxStack ms = new MaxStack ();
            ms.Add (1);
            ms.CurrentMax ();
            ms.Add (5);
            ms.CurrentMax ();
            ms.Add (3);
            ms.CurrentMax ();
            ms.Add (6);
            ms.CurrentMax ();
            ms.Add (4);
            ms.CurrentMax ();
            ms.Add (13);
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
            ms.Remove ();
            ms.CurrentMax ();
        }
    }

    public class MaxStack {
        private Stack<int> _s;
        private Stack<int> _m;

        public MaxStack () {
            _s = new Stack<int> ();
            _m = new Stack<int> ();
        }

        public void Add (int data) {
            this._s.Push (data);

            if (this._m.Count == 0 || this._m.Peek () < data) {
                this._m.Push (data);
            }
        }

        public int Remove () {
            int result = -1;

            if (this._s.Count > 0) {
                result = this._s.Pop ();

                if (this._m.Peek () == result) {
                    this._m.Pop ();
                }
            }

            return result;
        }

        public void CurrentMax () {
            if (this._m.Count > 0) {
                Console.WriteLine (this._m.Peek ());
            } else {
                Console.WriteLine ("No Value Present in Stack");
            }
        }
    }
}