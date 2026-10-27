// Url: https://www.geeksforgeeks.org/boggle-find-possible-words-board-characters/

/*
Boggle Board (Find all possible words in a board of characters)
Given a dictionary, a method to do lookup in dictionary and a M x N board where every cell has one character. Find all possible words that can be formed by a sequence of adjacent characters. Note that we can move to any of 8 adjacent characters, but a word should not have multiple instances of same cell.

Example:

Input: dictionary[] = {"GEEKS", "FOR", "QUIZ", "GO"};
       boggle[][]   = {{'G', 'I', 'Z'},
                       {'U', 'E', 'K'},
                       {'Q', 'S', 'E'}};
      isWord(str): returns true if str is present in dictionary
                   else false.

Output:  Following words of dictionary are present
         GEEKS
         QUIZ
*/

using System;

namespace InterviewPreperationGuide.Core.geeksForGeeks.boggleBoard
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(FindWords(new char[,] {
                { 'G', 'I', 'Z' },
                { 'U', 'E', 'K' },
                { 'Q', 'S', 'E' }}, ["GEEKS", "FOR", "QUIZ", "GUQ", "EE"]));
        }

        public static string[] FindWords(char[,] boggle, string[] dict)
        {
            return null;
        }
    }
}