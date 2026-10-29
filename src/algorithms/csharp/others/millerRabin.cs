/*
Miller–Rabin Primality Test
https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test

Write n - 1 = d * 2^r with d odd. For a witness a, n is "probably prime" if
a^d ≡ 1 (mod n) or a^(d * 2^i) ≡ -1 (mod n) for some 0 <= i < r.

Testing the first 12 primes as witnesses makes the test deterministic for every
64-bit integer (n < 3.3 * 10^24), so no randomness is required.
*/

using System;
using System.Numerics;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    public class MillerRabin {
        private static readonly ulong[] Witnesses = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37];

        public static void Init () {
            ulong[] tests = [0, 1, 2, 3, 4, 17, 561, 7919, 1_000_000_007, 999_999_999_989, 18_446_744_073_709_551_557];

            foreach (ulong n in tests) {
                Console.WriteLine ($"{n}: {IsPrime (n)}");
            }
            // 561 is a Carmichael number — it fools the Fermat test but not Miller–Rabin.
        }

        // Time: O (k * log^3 n), k = number of witnesses
        // Space: O (1)
        public static bool IsPrime (ulong n) {
            if (n < 2) {
                return false;
            }

            foreach (ulong p in Witnesses) {
                if (n % p == 0) {
                    return n == p;
                }
            }

            ulong d = n - 1;
            int r = 0;
            while ((d & 1) == 0) {
                d >>= 1;
                r++;
            }

            foreach (ulong a in Witnesses) {
                if (!PassesRound (a, d, r, n)) {
                    return false;
                }
            }

            return true;
        }

        private static bool PassesRound (ulong a, ulong d, int r, ulong n) {
            ulong x = ModPow (a, d, n);

            if (x == 1 || x == n - 1) {
                return true;
            }

            for (int i = 1; i < r; i++) {
                x = MulMod (x, x, n);
                if (x == n - 1) {
                    return true;
                }
            }

            return false;
        }

        private static ulong ModPow (ulong b, ulong e, ulong m) {
            ulong result = 1;
            b %= m;

            while (e > 0) {
                if ((e & 1) == 1) {
                    result = MulMod (result, b, m);
                }
                b = MulMod (b, b, m);
                e >>= 1;
            }

            return result;
        }

        // 128-bit intermediate avoids overflow when multiplying two 64-bit values.
        private static ulong MulMod (ulong a, ulong b, ulong m) {
            return (ulong) ((UInt128) a * b % m);
        }
    }
}
