/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.DataStructure.HashTable {
    public class CHashTable {
        private const int _defaultCapacity = 100;
        private const int _defaultSize = 4;
        private int _capacity;
        private ArrayList[] _items;

        public CHashTable (int? capacity) {
            if (capacity != null) {
                _capacity = (int) capacity;
            } else {
                _capacity = _defaultCapacity;
            }

            _items = new ArrayList[_capacity];

            for (int i = 0; i < _capacity; i++) {
                _items[i] = new ArrayList (_defaultSize);
            }
        }

        private int hashFunction (string str) {
            int result = 0;

            for (int i = 0; i < str.Length; i++) {
                result += (int) str[i];
            }

            result = result % this._items.GetUpperBound (0);

            return result;
        }

        public void add (string key, object value) {
            int hash = hashFunction (key);

            if (!_items[hash].Contains (value)) {
                _items[hash].Add (value);
            }
        }

        public void remove (string key) {
            int hash = hashFunction (key);

            if (_items[hash].Contains (key)) {
                _items[hash].Remove (key);
            }
        }
    }
}