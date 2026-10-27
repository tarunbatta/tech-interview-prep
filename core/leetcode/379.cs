// Url: https://leetcode.com/problems/design-phone-directory

/*
379. Design Phone Directory
Medium

Design a phone directory that initially has maxNumbers empty slots that can store numbers from 0 to maxNumbers - 1.

Implement the PhoneDirectory class:
- PhoneDirectory(int maxNumbers) Initializes the phone directory with the number of available slots maxNumbers.
- int get() Provides a number that is not assigned to anyone. Returns -1 if no number is available.
- bool check(int number) Returns true if the number is available for use, otherwise returns false.
- void release(int number) Recycles or releases a number.

Example 1:
Input
["PhoneDirectory", "get", "get", "check", "get", "check", "release", "check"]
[[3], [], [], [2], [], [2], [2], [2]]
Output
[null, 0, 1, true, 2, false, null, true]

Explanation
PhoneDirectory directory = new PhoneDirectory(3);
directory.get();      // It can return any available phone number. Here we assume it returns 0.
directory.get();      // Assume it returns 1.
directory.check(2);   // The number 2 is available, so return true.
directory.get();      // It returns 2, the only remaining number.
directory.check(2);   // The number 2 is no longer available, so return false.
directory.release(2); // Release number 2 back to the pool.
directory.check(2);   // Number 2 is available again, return true.

Constraints:
1 <= maxNumbers <= 10^4
0 <= number < maxNumbers
At most 2 * 10^4 calls will be made to get, check, and release.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem379
{
    public class Solution
    {
        public void Init()
        {
            PhoneDirectory directory = new(3);
            Console.WriteLine(directory.Get());      // 0
            Console.WriteLine(directory.Get());      // 1
            Console.WriteLine(directory.Check(2));   // True
            Console.WriteLine(directory.Get());      // 2
            Console.WriteLine(directory.Check(2));   // False
            directory.Release(2);
            Console.WriteLine(directory.Check(2));   // True
        }
    }

    // Time:
    //   Get: O(1)
    //   Check: O(1)
    //   Release: O(1)
    // Space: O(maxNumbers) for queue and boolean tracking array
    public class PhoneDirectory
    {
        private readonly Queue<int> _availableQueue;
        private readonly bool[] _isUsed;
        private readonly int _maxNumbers;

        public PhoneDirectory(int maxNumbers)
        {
            _maxNumbers = maxNumbers;
            _isUsed = new bool[maxNumbers];
            _availableQueue = new Queue<int>(maxNumbers);

            for (int i = 0; i < maxNumbers; i++)
            {
                _availableQueue.Enqueue(i);
            }
        }

        public int Get()
        {
            if (_availableQueue.Count == 0)
            {
                return -1;
            }

            int number = _availableQueue.Dequeue();
            _isUsed[number] = true;
            return number;
        }

        public bool Check(int number)
        {
            if (number < 0 || number >= _maxNumbers)
            {
                return false;
            }

            return !_isUsed[number];
        }

        public void Release(int number)
        {
            if (number < 0 || number >= _maxNumbers || !_isUsed[number])
            {
                return;
            }

            _isUsed[number] = false;
            _availableQueue.Enqueue(number);
        }
    }
}