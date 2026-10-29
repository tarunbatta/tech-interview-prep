// Url: https://leetcode.com/problems/minimum-number-of-food-buckets-to-feed-the-hamsters/

/*
2086. Minimum Number of Food Buckets to Feed the Hamsters
Medium

You are given a 0-indexed string hamsters where hamsters[i] is either:
- 'H' that means there is a hamster at index i, or
- '.' that means index i is empty.

You will also place buckets on empty spaces '.' to feed hamsters. A bucket can feed hamster at i if the bucket is at index i - 1 or i + 1.
Return the minimum number of food buckets you should place to feed all the hamsters, or -1 if it is impossible.

Example 1:
Input: hamsters = "H..H"
Output: 2
Explanation:
We place two food buckets at index 1 and 2.

Example 2:
Input: hamsters = ".H.H."
Output: 1
Explanation:
We place one food bucket at index 2 to feed both hamsters.

Example 3:
Input: hamsters = ".HHH."
Output: -1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem2086
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinimumBuckets(null));     // Expected: 0
            Console.WriteLine(MinimumBuckets("H..H"));   // Expected: 2
            Console.WriteLine(MinimumBuckets(".H.H."));  // Expected: 1
            Console.WriteLine(MinimumBuckets(".HHH."));  // Expected: -1
            Console.WriteLine(MinimumBuckets("."));      // Expected: 0
            Console.WriteLine(MinimumBuckets("H"));      // Expected: -1
        }

        // Time: O(n) where n is the length of hamsters string
        // Space: O(n) to store the character array
        public static int MinimumBuckets(string hamsters)
        {
            if (string.IsNullOrEmpty(hamsters))
            {
                return 0;
            }

            char[] arr = hamsters.ToCharArray();
            int n = arr.Length;
            int buckets = 0;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] == 'H')
                {
                    // If the hamster is already fed by a bucket on its left
                    if (i > 0 && arr[i - 1] == 'B')
                    {
                        continue;
                    }

                    // Greedily place a bucket to the right so it can also feed hamster at i + 2
                    if (i + 1 < n && arr[i + 1] == '.')
                    {
                        arr[i + 1] = 'B';
                        buckets++;
                    }
                    // Else place a bucket on the left if empty
                    else if (i > 0 && arr[i - 1] == '.')
                    {
                        arr[i - 1] = 'B';
                        buckets++;
                    }
                    // No adjacent empty space available -> impossible
                    else
                    {
                        return -1;
                    }
                }
            }

            return buckets;
        }
    }
}
