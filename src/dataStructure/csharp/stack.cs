/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.DataStructure.CustomStack {
    public class CStack {
        private int _index;
        private ArrayList _list;

        public CStack () {
            _list = [];
            _index = -1;
        }

        public void Push (object value) {
            _list.Add (value);
            _index++;
        }

        public object Pop () {
            if (_index == -1) {
                return null;
            } else {
                object item = _list[_index];
                _list.RemoveAt (_index);
                _index--;
                return item;
            }
        }

        public object Peek () {
            if (_index == -1) {
                return null;
            } else {
                return _list[_index];
            }
        }

        public int Count () {
            return _index + 1;
        }

        public void Clear () {
            _list.Clear ();
            _index = -1;
        }

        public bool IsEmpty () {
            return _index == -1 ? true : false;
        }
    }
}