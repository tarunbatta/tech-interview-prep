// Url:https://leetcode.com/problems/print-zero-even-odd

/*
1116. Print Zero Even Odd
Medium

You have a function printNumber that can be called with an integer parameter and prints it to the console.
- For example, calling printNumber(7) prints 7 to the console.

You are given an instance of the class ZeroEvenOdd that has three functions: zero, even, and odd. The same instance of ZeroEvenOdd will be passed to three different threads:
- Thread A: calls zero() that should only output 0's.
- Thread B: calls even() that should only output even numbers.
- Thread C: calls odd() that should only output odd numbers.

Modify the given class to output the series "010203040506..." where the length of the series must be 2n.

Implement the ZeroEvenOdd class:
- ZeroEvenOdd(int n) Initializes the object with the number n that represents the numbers that should be printed.
- void zero(printNumber) Calls printNumber to output one zero.
- void even(printNumber) Calls printNumber to output one even number.
- void odd(printNumber) Calls printNumber to output one odd number.

Example 1:
Input: n = 2
Output: "0102"
Explanation: There are three threads being fired asynchronously.
One of them calls zero(), the other calls even(), and the last one calls odd().
"0102" is the correct output.

Example 2:
Input: n = 5
Output: "0102030405"

Constraints:
- 1 <= n <= 1000
*/

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewPreperationGuide.Core.LeetCode.problem1116 {
    public class ZeroEvenOdd {
        private readonly int _n;
        private readonly SemaphoreSlim _zeroSem = new(1, 1);
        private readonly SemaphoreSlim _evenSem = new(0, 1);
        private readonly SemaphoreSlim _oddSem = new(0, 1);

        public ZeroEvenOdd(int n) {
            _n = n;
        }

        // Time: O(n)
        // Space: O(1)
        public void Zero(Action<int> printNumber) {
            for (int i = 1; i <= _n; i++) {
                _zeroSem.Wait();
                printNumber(0);
                if (i % 2 == 1) {
                    _oddSem.Release();
                } else {
                    _evenSem.Release();
                }
            }
        }

        // Time: O(n)
        // Space: O(1)
        public void Even(Action<int> printNumber) {
            for (int i = 2; i <= _n; i += 2) {
                _evenSem.Wait();
                printNumber(i);
                _zeroSem.Release();
            }
        }

        // Time: O(n)
        // Space: O(1)
        public void Odd(Action<int> printNumber) {
            for (int i = 1; i <= _n; i += 2) {
                _oddSem.Wait();
                printNumber(i);
                _zeroSem.Release();
            }
        }
    }

    public class Solution {
        public void Init() {
            StringBuilder sb = new();
            Action<int> print = val => sb.Append(val);

            ZeroEvenOdd zeo = new(5);
            var t1 = Task.Run(() => zeo.Zero(print));
            var t2 = Task.Run(() => zeo.Even(print));
            var t3 = Task.Run(() => zeo.Odd(print));

            Task.WaitAll(t1, t2, t3);
            Console.WriteLine(sb.ToString()); // "0102030405"
        }
    }
}