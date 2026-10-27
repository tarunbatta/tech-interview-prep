/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.DataStructure.CustomPriorityQueue {
    public struct Node {
        public int priority;
        public string name;
    }

    public class PriorityQueue : Queue {
        public PriorityQueue () : base () {

        }

        public override object Dequeue () {
            object[] items;
            int min, minindex;
            items = this.ToArray ();
            minindex = 0;
            min = ((Node) items[minindex]).priority;

            for (int x = 1; x <= items.GetUpperBound (0); x++) {
                Node item = (Node) items[x];

                if (item.priority < min) {
                    min = item.priority;
                    minindex = x;
                }
            }

            this.Clear ();

            for (int x = 0; x <= items.GetUpperBound (0); x++) {
                Node item = (Node) items[x];

                if (x != minindex && item.name != "") {
                    this.Enqueue (items[x]);
                }
            }

            return items[minindex];
        }
    }
}