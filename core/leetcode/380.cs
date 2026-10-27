using System.Collections;
// Url:https://leetcode.com/problems/insert-delete-getrandom-o1

/*
380. Insert Delete GetRandom O(1)
Medium

Design a data structure that supports all following operations in average O(1) time.

insert(val): Inserts an item val to the set if not already present.
remove(val): Removes an item val from the set if present.
getRandom: Returns a random element from current set of elements. Each element must have the same probability of being returned.


Example:
// Init an empty set.
RandomizedSet randomSet = new RandomizedSet();

// Inserts 1 to the set. Returns true as 1 was inserted successfully.
randomSet.insert(1);

// Returns false as 2 does not exist in the set.
randomSet.remove(2);

// Inserts 2 to the set, returns true. Set now contains [1,2].
randomSet.insert(2);

// getRandom should return either 1 or 2 randomly.
randomSet.getRandom();

// Removes 1 from the set, returns true. Set now contains [2].
randomSet.remove(1);

// 2 was already in the set, so return false.
randomSet.insert(2);

// Since 2 is the only number in the set, getRandom always return 2.
randomSet.getRandom();
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem380 {
    public class Solution {
        public static void Init () {
            RandomizedSet obj = new RandomizedSet ();
            bool param_1 = obj.Insert (0);
            bool param_2 = obj.Remove (0);
            int param_3 = obj.GetRandom ();
        }
    }

    public class RandomizedSet {
        Dictionary<int, int> _dict;
        List<int> _list;
        Random _random;

        public RandomizedSet () {
            _list = [];
            _dict = [];
            _random = new Random ();
        }

        // Inserts a value to the set. Returns true if the set did not already contain the specified element
        public bool Insert (int val) {
            if (_dict.ContainsKey (val)) {
                return false;
            }

            _list.Add (val);
            _dict.Add (val, _list.Count - 1);

            return true;
        }

        // Removes a value from the set. Returns true if the set contained the specified element
        public bool Remove (int val) {
            if (!_dict.ContainsKey (val)) {
                return false;
            }

            int index = _dict[val];
            _list[index] = _list[_list.Count - 1];
            _dict[_list[index]] = index;

            _list.RemoveAt (_list.Count - 1);
            _dict.Remove (val);

            return true;
        }

        // Get a random element from the set
        public int GetRandom () {
            return _list[_random.Next (_list.Count)];
        }
    }
}