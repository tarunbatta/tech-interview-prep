using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Maximum XOR of Two Numbers in an Array
    /// Given an array of integers. Find the maximum XOR of two numbers in given array. Expected time complexity O(n).
    ///
    /// Examples:
    /// Input: arr[] = {1, 2, 3, 4}
    /// Output: 7
    ///
    /// Input: arr[] = {8, 1, 2, 12, 7, 6}
    /// Output: 15
    ///
    /// Input: arr[] = {4, 6}
    /// Output: 2
    /// </summary>
    public class Question45 {
        public static void Init (string[] args) {
            Console.WriteLine (MaxSubarrayXOR_a (null));
            
            Console.WriteLine (MaxSubarrayXOR_a (new int[] { 1, 2, 3, 4 }));
            
            Console.WriteLine (MaxSubarrayXOR_a (new int[] { 8, 1, 2, 12, 7, 6 }));
            
            Console.WriteLine (MaxSubarrayXOR_a (new int[] { 4, 6 }));

            

            Console.WriteLine (MaxSubarrayXOR_b (null));
            
            Console.WriteLine (MaxSubarrayXOR_b (new int[] { 1, 2, 3, 4 }));
            
            Console.WriteLine (MaxSubarrayXOR_b (new int[] { 8, 1, 2, 12, 7, 6 }));
            
            Console.WriteLine (MaxSubarrayXOR_b (new int[] { 4, 6 }));
        }

        public static int MaxSubarrayXOR_a (int[] arr) {
            int result = 0;

            if (arr != null && arr.Length > 0) {
                for (int i = 0; i < arr.Length - 1; i++) {
                    for (int j = i; j < arr.Length; j++) {
                        result = Math.Max (arr[i] ^ arr[j], result);
                    }
                }
            }

            return result;
        }

        public static int MaxSubarrayXOR_b (int[] arr) {
            int result = 0;

            if (arr != null && arr.Length > 0) {
                MaxSubarrayXORTrie root = new MaxSubarrayXORTrie ();
                Add (root, 0);

                int pre_xor = 0;

                for (int i = 0; i < arr.Length; i++) {
                    // update current prefix xor and insert it into Trie
                    pre_xor = pre_xor ^ arr[i];

                    Add (root, pre_xor);

                    // Query for current prefix xor in Trie and update result if required
                    result = Math.Max (result, Search (root, pre_xor));
                }
            }

            return result;
        }

        private static void Add (MaxSubarrayXORTrie root, int pre_xor) {
            MaxSubarrayXORTrie temp = root;

            for (int i = sizeof (Int32) - 1; i >= 0; i--) {
                int val = pre_xor & (1 << i);

                if (temp.nodes[val] == null) {
                    temp.nodes[val] = new MaxSubarrayXORTrie ();
                }

                temp = temp.nodes[val];
            }
        }

        private static int Search (MaxSubarrayXORTrie root, int pre_xor) {
            MaxSubarrayXORTrie temp = root;

            for (int i = sizeof (Int32) - 1; i >= 0; i--) {
                // Find current bit in given prefix
                int val = pre_xor & (1 << i);

                // Traverse Trie, first look for a prefix that has opposite bit
                if (temp.nodes[1 - val] != null) {
                    temp = temp.nodes[1 - val];
                } else if (temp.nodes[val] != null) {
                    // If there is no prefix with opposite bit, then look for same bit.
                    temp = temp.nodes[val];
                }
            }

            return pre_xor ^ (temp.value);
        }
    }

    public class MaxSubarrayXORTrie {
        public int value = 0;
        public MaxSubarrayXORTrie[] nodes = new MaxSubarrayXORTrie[2];
    }
}