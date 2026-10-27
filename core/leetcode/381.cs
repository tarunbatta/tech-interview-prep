// Url:https://leetcode.com/problems/insert-delete-getrandom-o1-duplicates-allowed

/*
381. Insert Delete GetRandom O(1) - Duplicates allowed
Hard

Design a data structure that supports all following operations in average O(1) time.
Note: Duplicate elements are allowed.


insert(val): Inserts an item val to the collection.
remove(val): Removes an item val from the collection if present.
getRandom: Returns a random element from current collection of elements. The probability of each element being returned is linearly related to the number of same value the collection contains.



Example:
// Init an empty collection.
RandomizedCollection collection = new RandomizedCollection();

// Inserts 1 to the collection. Returns true as the collection did not contain 1.
collection.insert(1);

// Inserts another 1 to the collection. Returns false as the collection contained 1. Collection now contains [1,1].
collection.insert(1);

// Inserts 2 to the collection, returns true. Collection now contains [1,1,2].
collection.insert(2);

// getRandom should return 1 with the probability 2/3, and returns 2 with the probability 1/3.
collection.getRandom();

// Removes 1 from the collection, returns true. Collection now contains [1,2].
collection.remove(1);

// getRandom should return 1 and 2 both equally likely.
collection.getRandom();
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem381 {
    public class Solution {
        public static void Init () {
            RandomizedCollection obj = new RandomizedCollection ();
            bool param_1 = RandomizedCollection.Insert(0);
            bool param_2 = RandomizedCollection.Remove(0);
            int param_3 = RandomizedCollection.GetRandom();
        }
    }

    public class RandomizedCollection {
        public RandomizedCollection () {

        }

        // Inserts a value to the collection. Returns true if the collection did not already contain the specified element
        public static bool Insert (int val) {
            return false;
        }

        // Removes a value from the collection. Returns true if the collection contained the specified element
        public static bool Remove (int val) {
            return false;
        }

        // Get a random element from the collection
        public static int GetRandom () {
            return 0;
        }
    }
}