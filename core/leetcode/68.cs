// Url:https://leetcode.com/problems/text-justification

/*
68. Text Justification
Hard

Given an array of words and a width maxWidth, format the text such that each line has exactly maxWidth characters and is fully (left and right) justified.

You should pack your words in a greedy approach; that is, pack as many words as you can in each line. Pad extra spaces ' ' when necessary so that each line has exactly maxWidth characters.

Extra spaces between words should be distributed as evenly as possible. If the number of spaces on a line do not divide evenly between words, the empty slots on the left will be assigned more spaces than the slots on the right.

For the last line of text, it should be left justified and no extra space is inserted between words.

Note:
	A word is defined as a character sequence consisting of non-space characters only.
	Each word's length is guaranteed to be greater than 0 and not exceed maxWidth.
	The input array words contains at least one word.


Example 1:

Input:
words = ["This", "is", "an", "example", "of", "text", "justification."]
maxWidth = 16
Output:
[
   "This    is    an",
   "example  of text",
   "justification.  "
]


Example 2:

Input:
words = ["What","must","be","acknowledgment","shall","be"]
maxWidth = 16
Output:
[
  "What   must   be",
  "acknowledgment  ",
  "shall be        "
]

Explanation: Note that the last line is "shall be    " instead of "shall     be",
             because the last line must be left-justified instead of fully-justified.
             Note that the second line is also left-justified becase it contains only one word.


Example 3:

Input:
words = ["Science","is","what","we","understand","well","enough","to","explain",
         "to","a","computer.","Art","is","everything","else","we","do"]
maxWidth = 20
Output:
[
  "Science  is  what we",
  "understand      well",
  "enough to explain to",
  "a  computer.  Art is",
  "everything  else  we",
  "do                  "
]
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem68 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (FullJustify_a(["This", "is", "an", "example", "of", "text", "justification."], 16));
            Console.WriteLine (FullJustify_a(["What", "must", "be", "acknowledgment", "shall", "be"], 16));
            Console.WriteLine (FullJustify_a(["Science", "is", "what", "we", "understand", "well", "enough", "to", "explain", "to", "a", "computer.", "Art", "is", "everything", "else", "we", "do"], 20));
        }

        public static IList<string> FullJustify_a (string[] words, int maxWidth) {
            IList<string> result = [];

            if (words == null || words.Length == 0) {
                return result;
            }

            int start = 0;
            int end = 0;
            int currentWidth = words[0].Length;

            for (int i = 1; i < words.Length; i++) {
                if (currentWidth + (end - start) + words[i].Length <= maxWidth) {
                    end = i;
                    currentWidth += words[i].Length;
                } else {
                    int numberOfItems = end - start == 0 ? 1 : end - start;
                    currentWidth += end - start;
                    int additionalSpaces = (maxWidth - currentWidth) / numberOfItems;
                    int unevenSpaces = (maxWidth - currentWidth) % numberOfItems;
                    StringBuilder line = new();

                    for (int j = start; j <= end; j++) {
                        int spacesCount = additionalSpaces + (end == start ? 0 : 1);

                        if (unevenSpaces > 0) {
                            spacesCount++;
                            unevenSpaces--;
                        }

                        if (j == end && start != end) {
                            spacesCount = 0;
                        }

                        line.Append (words[j]);

                        for (int k = 1; k <= spacesCount; k++) {
                            line.Append (' ');
                        }
                    }

                    result.Add (line.ToString ());
                    start = i;
                    end = i;
                    currentWidth = words[i].Length;
                }
            }

            if (currentWidth > 0) {
                StringBuilder line = new();

                for (int j = start; j <= end; j++) {
                    line.Append (words[j]);

                    if (line.Length < maxWidth) {
                        line.Append (' ');
                    }
                }

                while (line.Length < maxWidth) {
                    line.Append (' ');
                }

                result.Add (line.ToString ());
            }

            return result;
        }

        public static IList<string> FullJustify_b (string[] words, int maxWidth) {
            var result = new List<string> ();

            var left = 0;
            var right = 0;
            var curLength = 0;

            while (right < words.Length) {
                curLength += words[right].Length;

                if (curLength > maxWidth) {
                    right--;

                    result.Add (FillLine(words, left, right, maxWidth));

                    left = right + 1;

                    curLength = 0;
                } else {
                    curLength += 1;
                }

                right++;
            }

            result.Add (FillLastLine(words, left, maxWidth));

            return result;
        }

        private static string FillLine (string[] words, int startIndex, int endIndex, int maxWidth) {
            if (endIndex - startIndex == 0) {
                return $"{words[startIndex]}{new string(' ', maxWidth - words[startIndex].Length)}";
            }

            var wordsLength = 0;
            for (int i = startIndex; i <= endIndex; i++) {
                wordsLength += words[i].Length;
            }

            var restSpace = maxWidth - wordsLength;
            var spaceCount = endIndex - startIndex + 1 - 1;

            var oneResult = new List<string> ();

            for (int i = endIndex; i > startIndex; i--) {
                oneResult.Add (words[i]);
                oneResult.Add (new string (' ', restSpace / spaceCount));

                restSpace -= restSpace / spaceCount;
                spaceCount--;
            }

            oneResult.Add (words[startIndex]);
            oneResult.Reverse ();

            return string.Join ("", oneResult);
        }

        private static string FillLastLine (string[] words, int startIndex, int maxWidth) {
            var restSpace = maxWidth;
            var result = new List<string> ();
            for (int i = startIndex; i < words.Length - 1; i++) {
                result.Add (words[i]);
                result.Add (" ");

                restSpace -= words[i].Length;
                restSpace -= 1;
            }

            result.Add (words[^1]);

            restSpace -= words[^1].Length;

            result.Add (new string (' ', restSpace));

            return string.Join ("", result);
        }
    }
}