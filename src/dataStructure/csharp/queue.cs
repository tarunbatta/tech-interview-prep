/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.DataStructure.CustomQueue {
    public class CQueue {
        private ArrayList _list;

        public CQueue () {
            _list = [];
        }

        public void EnQueue (object item) {
            _list.Add (item);
        }

        public object DeQueue () {
            if (this.IsEmpty ()) {
                return null;
            } else {
                object item = _list[0];
                _list.RemoveAt (0);
                return item;
            }
        }

        public object Peek () {
            if (this.IsEmpty ()) {
                return null;
            } else {
                return _list[0];
            }
        }

        public int Count () {
            return _list.Count;
        }

        public void Clear () {
            _list.Clear ();
        }

        public bool IsEmpty () {
            return _list.Count == 0 ? true : false;
        }
    }
}