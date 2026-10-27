// Url:https://leetcode.com/problems/minimize-rounding-error-to-meet-target

/*
1058. Minimize Rounding Error to Meet Target
Medium

Given an array of prices as strings and an integer target, round each price to either Floor(price) or Ceil(price) such that the sum of the rounded numbers equals the given target. Each operation will have a rounding error: abs(price - rounded).

Return the minimum rounding error rounded to 3 decimal places formatted as a string. If it is impossible for the sum of the rounded numbers to equal target, return "-1".

Example 1:
Input: prices = ["0.700","2.800","4.900"], target = 8
Output: "1.000"
Explanation: 
Use Floor, Ceil, and Ceil, respectively:
[0.700, 2.800, 4.900] -> [0, 3, 5]
So the sum is 8 and total error is |0.7 - 0| + |2.8 - 3| + |4.9 - 5| = 0.7 + 0.2 + 0.1 = 1.0.

Example 2:
Input: prices = ["1.500","2.500","3.500"], target = 10
Output: "-1"
Explanation: It is impossible to meet the target.

Constraints:
- 1 <= prices.length <= 500
- Each string prices[i] represents a real number in the range [0.0, 1000.0] and has exactly 3 decimal places.
- 0 <= target <= 10^6
*/

using System;
using System.Collections.Generic;
using System.Globalization;

namespace InterviewPreperationGuide.Core.LeetCode.problem1058 {
    public class Solution {
        public void Init() {
            string[] prices1 = ["0.700", "2.800", "4.900"];
            Console.WriteLine(MinimizeError(prices1, 8)); // "1.000"

            string[] prices2 = ["1.500", "2.500", "3.500"];
            Console.WriteLine(MinimizeError(prices2, 10)); // "-1"
        }

        // Time: O(n log n) where n is the number of prices
        // Space: O(n) to store fractional parts
        public string MinimizeError(string[] prices, int target) {
            int minSum = 0;
            int maxSum = 0;
            double baseError = 0;
            List<double> fractions = [];

            foreach (string p in prices) {
                double val = double.Parse(p, CultureInfo.InvariantCulture);
                int floor = (int)Math.Floor(val);
                int ceil = (int)Math.Ceiling(val);

                minSum += floor;
                maxSum += ceil;

                double frac = val - floor;
                baseError += frac;

                if (floor != ceil) {
                    fractions.Add(frac);
                }
            }

            if (target < minSum || target > maxSum) {
                return "-1";
            }

            int neededCeils = target - minSum;
            fractions.Sort((a, b) => b.CompareTo(a)); // Sort descending

            double totalError = baseError;
            for (int i = 0; i < neededCeils; i++) {
                totalError += (1.0 - 2.0 * fractions[i]);
            }

            return totalError.ToString("F3", CultureInfo.InvariantCulture);
        }
    }
}