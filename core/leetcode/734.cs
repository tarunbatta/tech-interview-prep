// Url:https://leetcode.com/problems/sentence-similarity

/*
734. Sentence Similarity
Easy

We can represent a sentence as an array of strings, for example, the sentence "I am happy with leetcode" can be represented as arr = ["I","am","happy","with","leetcode"].

Given two sentences sentence1 and sentence2 each represented as a string array and given an array of string pairs similarPairs where similarPairs[i] = [xi, yi] indicates that the two words xi and yi are similar.

Return true if sentence1 and sentence2 are similar, or false if they are not.

Two sentences are similar if:
- They have the same length (i.e., the same number of words)
- sentence1[i] and sentence2[i] are similar.

Notice that a word is always similar to itself, also notice that the similarity relation is not transitive. For example, if the words a and b are similar, and the words b and c are similar, a and c are not necessarily similar.

Example 1:
Input: sentence1 = ["great","acting","skills"], sentence2 = ["fine","drama","talent"], similarPairs = [["great","fine"],["drama","acting"],["skills","talent"]]
Output: true

Example 2:
Input: sentence1 = ["great"], sentence2 = ["great"], similarPairs = []
Output: true

Example 3:
Input: sentence1 = ["great"], sentence2 = ["doubleplus","good"], similarPairs = [["great","doubleplus"]]
Output: false

Constraints:
- 1 <= sentence1.length, sentence2.length <= 1000
- 1 <= sentence1[i].length, sentence2[i].length <= 20
- sentence1[i] and sentence2[i] consist of English letters.
- 0 <= similarPairs.length <= 1000
- similarPairs[i].length == 2
- 1 <= xi.length, yi.length <= 20
- xi and yi consist of English letters.
- All the pairs (xi, yi) are distinct.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem734 {
    public class Solution {
        public void Init() {
            string[] s1 = ["great", "acting", "skills"];
            string[] s2 = ["fine", "drama", "talent"];
            IList<IList<string>> pairs = [
                ["great", "fine"],
                ["drama", "acting"],
                ["skills", "talent"]
            ];
            Console.WriteLine(AreSentencesSimilar(s1, s2, pairs)); // True
        }

        // Time: O(N + P) where N is words count and P is pairs count
        // Space: O(P) to store similar pairs
        public bool AreSentencesSimilar(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs) {
            if (sentence1.Length != sentence2.Length) {
                return false;
            }

            HashSet<(string, string)> pairs = [];
            foreach (var pair in similarPairs) {
                pairs.Add((pair[0], pair[1]));
                pairs.Add((pair[1], pair[0]));
            }

            for (int i = 0; i < sentence1.Length; i++) {
                if (sentence1[i] == sentence2[i]) {
                    continue;
                }

                if (!pairs.Contains((sentence1[i], sentence2[i]))) {
                    return false;
                }
            }

            return true;
        }
    }
}