// Url:https://leetcode.com/problems/reorganize-string

/*
767. Reorganize String
Medium

Given a string S, check if the letters can be rearranged so that two characters that are adjacent to each other are not the same.

If possible, output any possible result.  If not possible, return the empty string.

Example 1:

Input: S = "aab"
Output: "aba"


Example 2:

Input: S = "aaab"
Output: ""


Note:
S will consist of lowercase letters and have length in range [1, 500].
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem767 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n log a), where n is length of string
        // Space: O (a), where a is size of alphabet
        public static string ReorganizeString (string S) {
            if (string.IsNullOrEmpty (S)) {
                return "";
            }

            Dictionary<char, int> dict = [];
            for (int i = 0; i < S.Length; i++) {
                if (!dict.ContainsKey (S[i]))
                    dict.Add (S[i], 0);
                dict[S[i]]++;
            }

            var heaptree = new HeapDS ();
            foreach (var kvp in dict) {
                heaptree.add (new charDetails (kvp.Key, kvp.Value));
            }

            var sb = new StringBuilder ();
            char lastchar = '\0';
            while (heaptree.size () >= 2) {
                var item1 = heaptree.poll ();
                var item2 = heaptree.poll ();

                if (lastchar != item1.value) {
                    sb.Append (item1.value);
                    --item1.count;
                }
                if (item1.count > 0) heaptree.add (item1);

                sb.Append (item2.value);
                lastchar = item2.value;
                if (--item2.count > 0) heaptree.add (item2);
            }

            if (heaptree.size () > 0) {
                var item = heaptree.poll ();
                if (item.count > 1) return "";
                sb.Append (item.value);
            }

            return sb.ToString ();
        }
    }

    public class HeapDS {
        public List<charDetails> lstHeap;
        public HeapDS () {
            lstHeap = [];
        }

        public void add (charDetails node) {
            lstHeap.Insert (0, node);
            heapifyDown ();
        }
        public int size () {
            return lstHeap.Count;
        }
        public charDetails poll () {
            var item = lstHeap[0];
            lstHeap[0] = lstHeap[lstHeap.Count - 1];
            lstHeap.RemoveAt (lstHeap.Count - 1);
            heapifyDown ();
            return item;
        }
        private void heapifyUp () {
            if (lstHeap.Count <= 1) return;
            var index = lstHeap.Count - 1;
            int parentIndex = (index - 1) / 2;
            while (parentIndex > 0) {
                if (lstHeap[index].count < lstHeap[parentIndex].count ||
                    lstHeap[index].count == lstHeap[parentIndex].count &&
                    lstHeap[index].value > lstHeap[parentIndex].value) {
                    break;
                }
                swap (parentIndex, index);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
            if (lstHeap[index].count > lstHeap[parentIndex].count) {
                swap (parentIndex, index);
            }
        }

        private void swap (int parent, int child) {
            var temp = lstHeap[parent];
            lstHeap[parent] = lstHeap[child];
            lstHeap[child] = temp;
        }

        private void heapifyDown () {
            if (lstHeap.Count <= 1) return;
            var parentIndex = 0;
            var lchildIndex = (2 * parentIndex + 1);
            var rchildIndex = (2 * parentIndex + 2);
            var swapindex = 0;
            while (lchildIndex < lstHeap.Count) {
                swapindex = lchildIndex;
                if (rchildIndex > lstHeap.Count &&
                    lstHeap[lchildIndex].count < lstHeap[rchildIndex].count) {
                    swapindex = rchildIndex;
                }
                if (lstHeap[swapindex].count < lstHeap[parentIndex].count ||
                    lstHeap[swapindex].count == lstHeap[parentIndex].count &&
                    lstHeap[swapindex].value > lstHeap[parentIndex].value) {
                    break;
                }
                swap (parentIndex, swapindex);
                parentIndex = swapindex;
                lchildIndex = (2 * parentIndex + 1);
                rchildIndex = (2 * parentIndex + 2);
            }
        }
    }

    public class charDetails {
        public char value;
        public int count;
        public charDetails (char value, int count) {
            this.value = value;
            this.count = count;
        }
    }
}