// Url: https://leetcode.com/problems/sort-transformed-array

/*
360. Sort Transformed Array
Medium

Given a sorted integer array nums and three integers a, b and c, apply a quadratic function of the form f(x) = ax^2 + bx + c to each element nums[i] in the array, and return the array in a sorted order.

Example 1:
Input: nums = [-4,-2,2,4], a = 1, b = 3, c = 5
Output: [3,9,15,33]

Example 2:
Input: nums = [-4,-2,2,4], a = -1, b = 3, c = 5
Output: [-23,-5,1,7]

Constraints:
1 <= nums.length <= 200
-100 <= nums[i], a, b, c <= 100
nums is sorted in ascending order.

Follow up: Could you solve it in O(n) time?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem360
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(string.Join(", ", SortTransformedArray([-4, -2, 2, 4], 1, 3, 5)));  // 3, 9, 15, 33
            Console.WriteLine(string.Join(", ", SortTransformedArray([-4, -2, 2, 4], -1, 3, 5))); // -23, -5, 1, 7
        }

        // Time: O(n) where n is nums.Length (two-pointer approach on parabolic curve)
        // Space: O(1) auxiliary space (excluding result array)
        public static int[] SortTransformedArray(int[] nums, int a, int b, int c)
        {
            if (nums == null || nums.Length == 0)
            {
                return [];
            }

            int n = nums.Length;
            int[] result = new int[n];
            int left = 0;
            int right = n - 1;

            if (a >= 0)
            {
                // Parabola opens upwards: largest values are at the ends
                int idx = n - 1;

                while (left <= right)
                {
                    int valLeft = Quad(nums[left], a, b, c);
                    int valRight = Quad(nums[right], a, b, c);

                    if (valLeft >= valRight)
                    {
                        result[idx--] = valLeft;
                        left++;
                    }
                    else
                    {
                        result[idx--] = valRight;
                        right--;
                    }
                }
            }
            else
            {
                // Parabola opens downwards: smallest values are at the ends
                int idx = 0;

                while (left <= right)
                {
                    int valLeft = Quad(nums[left], a, b, c);
                    int valRight = Quad(nums[right], a, b, c);

                    if (valLeft <= valRight)
                    {
                        result[idx++] = valLeft;
                        left++;
                    }
                    else
                    {
                        result[idx++] = valRight;
                        right--;
                    }
                }
            }

            return result;
        }

        private static int Quad(int x, int a, int b, int c)
        {
            return a * x * x + b * x + c;
        }
    }
}