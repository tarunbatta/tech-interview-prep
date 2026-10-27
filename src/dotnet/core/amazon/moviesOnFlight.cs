/*
Amazon OA: Movies on Flight

You are on a flight and want to watch two movies during this flight.
You are given int[] movieDurations which includes all the movie durations.
You are also given the duration of the flight which is d in minutes.
Pick two movies such that the total runtime is less than or equal to (d - 30min) — the 30 minutes are reserved
for take-off and landing.

Find the pair of movies with the longest total duration. If multiple such pairs exist, return the pair that
contains the single longest movie. Return the indices of the movies, or [-1, -1] if no pair exists.

Example 1:
Input: movieDurations = [90, 85, 75, 60, 120, 150, 125], d = 250
Output: [0, 6]
Explanation: movieDurations[0] + movieDurations[6] = 90 + 125 = 215 is the maximum number within 220 (250min - 30min)

Example 2:
Input: movieDurations = [90, 85, 75, 60, 155, 150, 125], d = 250
Output: [3, 4]
Explanation: 90 + 125 = 215 and 60 + 155 = 215 tie. Prefer the pair containing the longest movie: [3, 4]

(The same pattern is asked as "Find Pair With Given Sum" / "Sort Center".)
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.Amazon {
    public class MoviesOnFlight {
        public static void Init () {
            Print (Solve ([90, 85, 75, 60, 120, 150, 125], 250)); // [0, 6]
            Print (Solve ([90, 85, 75, 60, 155, 150, 125], 250)); // [3, 4]
            Print (Solve ([100, 200], 100)); // [-1, -1]
        }

        // Sort indices by duration, then two pointers from both ends.
        // Time: O (n log n)
        // Space: O (n)
        public static int[] Solve (int[] durations, int flight) {
            int target = flight - 30;
            int[] idx = Enumerable.Range (0, durations.Length).OrderBy (i => durations[i]).ToArray ();

            int lo = 0, hi = idx.Length - 1;
            int bestSum = -1, bestLongest = -1;
            int[] best = [-1, -1];

            while (lo < hi) {
                int a = durations[idx[lo]], b = durations[idx[hi]];
                int sum = a + b;

                if (sum > target) {
                    hi--;
                    continue;
                }

                int longest = Math.Max (a, b);
                if (sum > bestSum || (sum == bestSum && longest > bestLongest)) {
                    bestSum = sum;
                    bestLongest = longest;
                    best = [Math.Min (idx[lo], idx[hi]), Math.Max (idx[lo], idx[hi])];
                }

                lo++;
            }

            return best;
        }

        private static void Print (int[] pair) {
            Console.WriteLine ($"[{pair[0]}, {pair[1]}]");
        }
    }
}
