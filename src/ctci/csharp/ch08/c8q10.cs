/*
Paint Fill: 

Implement the "paint fill" function that one might see on many image editing programs.
That is, given a screen (represented by a two-dimensional array of colors), a point, and a new color,
fill in the surrounding area until the color changes from the original color.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c8q10 {
    public class Solution {
        public static bool Fill (int[][] colors, int r, int c, int newColor) {
            if (r < 0 || r >= colors.Length || c < 0 || c >= colors[0].Length) return false;
            if (colors[r][c] == newColor) return false;
            return FillHelper (colors, r, c, colors[r][c], newColor);
        }

        private static bool FillHelper (int[][] colors, int r, int c, int oldColor, int newColor) {
            if (r < 0 || r >= colors.Length || c < 0 || c >= colors[0].Length) return false;
            if (colors[r][c] == oldColor) {
                colors[r][c] = newColor;
                FillHelper (colors, r + 1, c, oldColor, newColor);
                FillHelper (colors, r - 1, c, oldColor, newColor);
                FillHelper (colors, r, c + 1, oldColor, newColor);
                FillHelper (colors, r, c - 1, oldColor, newColor);
            }
            return true;
        }

        public static void Init (string[] args = null) {
            int[][] screen = new int[][] {
                new int[] { 0, 1, 2, 1 },
                new int[] { 0, 1, 1, 1 },
                new int[] { 1, 1, 2, 2 }
            };
            Console.WriteLine ($"Color at (1, 1) before: {screen[1][1]}");
            Fill (screen, 1, 1, 5);
            Console.WriteLine ($"Color at (1, 1) after: {screen[1][1]}");
        }
    }
}
