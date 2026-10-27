// Url:https://leetcode.com/problems/lexicographically-smallest-equivalent-string

/*
1061. Lexicographically Smallest Equivalent String
Medium

You are given two strings of the same length s1 and s2 and a string baseStr.

We say s1[i] and s2[i] are equivalent characters.
- For example, if s1 = "abc" and s2 = "cde", then we have 'a' == 'c', 'b' == 'd', and 'c' == 'e'.

Equivalent characters follow the usual conditions of any equivalence relation:
- Reflexivity: 'a' == 'a'.
- Symmetry: 'a' == 'b' implies 'b' == 'a'.
- Transitivity: 'a' == 'b' and 'b' == 'c' implies 'a' == 'c'.

For example, given the equivalency information from s1 and s2 above, S = "cde", "acd" and "aab" are also equivalent strings of baseStr = "eed", and "aab" is the lexicographically smallest equivalent string of baseStr.

Return the lexicographically smallest equivalent string of baseStr by using the equivalency information from s1 and s2.

Example 1:
Input: s1 = "parker", s2 = "morris", baseStr = "parser"
Output: "makkek"
Explanation: Based on the given information, we have equivalence groups
[m,p], [a,o], [k,r,s], [e,i].
The characters in parser are:
p is equivalent to m, so choose m.
a is equivalent to a, so choose a.
r is equivalent to k, so choose k.
s is equivalent to k, so choose k.
e is equivalent to e, so choose e.
r is equivalent to k, so choose k.

Example 2:
Input: s1 = "hello", s2 = "world", baseStr = "hold"
Output: "hdld"

Example 3:
Input: s1 = "leetcode", s2 = "programs", baseStr = "sourcecode"
Output: "aauaaaaada"

Constraints:
- 1 <= s1.length, s2.length, baseStr.length <= 1000
- s1.length == s2.length
- s1, s2, and baseStr consist of 'a'-'z'.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem1061 {
    public class Solution {
        public void Init() {
            Console.WriteLine(SmallestEquivalentString("parker", "morris", "parser")); // "makkek"
            Console.WriteLine(SmallestEquivalentString("hello", "world", "hold")); // "hdld"
            Console.WriteLine(SmallestEquivalentString("leetcode", "programs", "sourcecode")); // "aauaaaaada"
        }

        // Time: O(N * alpha(26)) = O(N) where N is the length of strings
        // Space: O(1) fixed 26 letters parent array
        public string SmallestEquivalentString(string s1, string s2, string baseStr) {
            int[] parent = new int[26];
            for (int i = 0; i < 26; i++) {
                parent[i] = i;
            }

            int Find(int i) {
                if (parent[i] != i) {
                    parent[i] = Find(parent[i]);
                }
                return parent[i];
            }

            void Union(int i, int j) {
                int rootI = Find(i);
                int rootJ = Find(j);
                if (rootI != rootJ) {
                    if (rootI < rootJ) {
                        parent[rootJ] = rootI;
                    } else {
                        parent[rootI] = rootJ;
                    }
                }
            }

            for (int i = 0; i < s1.Length; i++) {
                Union(s1[i] - 'a', s2[i] - 'a');
            }

            StringBuilder sb = new();
            foreach (char c in baseStr) {
                sb.Append((char)('a' + Find(c - 'a')));
            }

            return sb.ToString();
        }
    }
}