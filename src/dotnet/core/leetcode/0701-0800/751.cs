// Url:https://leetcode.com/problems/ip-to-cidr

/*
751. IP to CIDR
Medium

An IP address is a formatted 32-bit unsigned integer where each group of 8 bits is printed as a decimal number and the dot character '.' splits each group. For example, the binary number 00001111 10001000 11111111 01101011 written as an IP address is "15.136.255.107".

A CIDR block is a format consisting of an IP, a slash, and a prefix length. For example, "123.45.67.89/20" is a CIDR block with prefix length of 20.

Given a starting IP address ip and the number of IPs we need to cover n, return the smallest list of CIDR blocks that covers the range of IPs [ip, ip + n - 1] exactly. There should not be any IP in the CIDR blocks that is not in the range [ip, ip + n - 1], and the range of IPs should be covered completely.

Example 1:
Input: ip = "255.0.0.7", n = 10
Output: ["255.0.0.7/32","255.0.0.8/29","255.0.0.16/32"]

Example 2:
Input: ip = "117.145.102.62", n = 8
Output: ["117.145.102.62/31","117.145.102.64/30","117.145.102.68/31"]

Constraints:
- 7 <= ip.length <= 15
- ip is a valid IPv4 address.
- 1 <= n <= 1000
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem751 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(string.Join(", ", IpToCIDR("255.0.0.7", 10)));
            // "255.0.0.7/32", "255.0.0.8/29", "255.0.0.16/32"

            Console.WriteLine(string.Join(", ", IpToCIDR("117.145.102.62", 8)));
            // "117.145.102.62/31", "117.145.102.64/30", "117.145.102.68/31"
        }

        // Time: O(log n)
        // Space: O(log n)
        public static IList<string> IpToCIDR(string ip, int n) {
            long start = IpToLong(ip);
            List<string> result = [];

            while (n > 0) {
                // Find the lowest set bit in start
                long lowestBit = start & (-start);
                if (lowestBit == 0) {
                    lowestBit = 1L << 32;
                }

                while (lowestBit > n) {
                    lowestBit >>= 1;
                }

                int prefixLen = 32 - BitLength(lowestBit) + 1;
                result.Add($"{LongToIp(start)}/{prefixLen}");

                start += lowestBit;
                n -= (int)lowestBit;
            }

            return result;
        }

        private static long IpToLong(string ip) {
            string[] parts = ip.Split('.');
            long val = 0;
            foreach (string part in parts) {
                val = (val << 8) + long.Parse(part);
            }
            return val;
        }

        private static string LongToIp(long val) {
            return $"{(val >> 24) & 255}.{(val >> 16) & 255}.{(val >> 8) & 255}.{val & 255}";
        }

        private static int BitLength(long val) {
            int len = 0;
            while (val > 0) {
                len++;
                val >>= 1;
            }
            return len;
        }
    }
}