// Url:https://leetcode.com/problems/sentence-similarity-ii

/*
737. Sentence Similarity II
Medium

We can represent a sentence as an array of strings, for example, the sentence "I am happy with leetcode" can be represented as arr = ["I","am","happy","with","leetcode"].

Given two sentences sentence1 and sentence2 each represented as a string array and given an array of string pairs similarPairs where similarPairs[i] = [xi, yi] indicates that the two words xi and yi are similar.

Return true if sentence1 and sentence2 are similar, or false if they are not.

Two sentences are similar if:
- They have the same length (i.e., the same number of words)
- sentence1[i] and sentence2[i] are similar.

Notice that a word is always similar to itself, also notice that the similarity relation is transitive. For example, if the words a and b are similar, and the words b and c are similar, then a and c are similar.

Example 1:
Input: sentence1 = ["great","acting","skills"], sentence2 = ["fine","drama","talent"], similarPairs = [["great","good"],["fine","good"],["acting","drama"],["skills","talent"]]
Output: true
Explanation: The two sentences of the same length and:
- "great" is similar to "good", "fine" is similar to "good", so "great" and "fine" are similar.
- "acting" and "drama" are similar.
- "skills" and "talent" are similar.
So, the two sentences are similar.

Example 2:
Input: sentence1 = ["I","love","leetcode"], sentence2 = ["I","love","onepiece"], similarPairs = [["manga","hunterXhunter"],["platform","anime"]]
Output: false
Explanation: "leetcode" is not similar to "onepiece".

Constraints:
- 1 <= sentence1.length, sentence2.length <= 1000
- 1 <= sentence1[i].length, sentence2[i].length <= 20
- sentence1[i] and sentence2[i] consist of English letters.
- 0 <= similarPairs.length <= 2000
- similarPairs[i].length == 2
- 1 <= xi.length, yi.length <= 20
- xi and yi consist of English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem737 {
    public class Solution {
        public void Init() {
            string[] s1 = ["great", "acting", "skills"];
            string[] s2 = ["fine", "drama", "talent"];
            IList<IList<string>> pairs = [
                ["great", "good"],
                ["fine", "good"],
                ["acting", "drama"],
                ["skills", "talent"]
            ];
            Console.WriteLine(AreSentencesSimilarTwo(s1, s2, pairs)); // True
        }

        // Time: O(N + P * alpha(P))
        // Space: O(P) to store disjoint set parent map
        public bool AreSentencesSimilarTwo(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs) {
            if (sentence1.Length != sentence2.Length) {
                return false;
            }

            Dictionary<string, string> parent = [];

            string Find(string x) {
                if (!parent.ContainsKey(x)) {
                    parent[x] = x;
                }
                if (parent[x] != x) {
                    parent[x] = Find(parent[x]);
                }
                return parent[x];
            }

            void Union(string x, string y) {
                string rootX = Find(x);
                string rootY = Find(y);
                if (rootX != rootY) {
                    parent[rootX] = rootY;
                }
            }

            foreach (var pair in similarPairs) {
                Union(pair[0], pair[1]);
            }

            for (int i = 0; i < sentence1.Length; i++) {
                if (sentence1[i] == sentence2[i]) {
                    continue;
                }

                if (Find(sentence1[i]) != Find(sentence2[i])) {
                    return false;
                }
            }

            return true;
        }
    }
}