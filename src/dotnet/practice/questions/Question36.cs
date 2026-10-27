using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find if two rectangles overlap:
    /// Given two rectangles, find if the given two rectangles overlap or not.
    /// Note that a rectangle can be represented by two coordinates, top left and bottom right.So mainly we are given following four coordinates.
    /// l1: Top Left coordinate of first rectangle.
    /// r1: Bottom Right coordinate of first rectangle.
    /// l2: Top Left coordinate of second rectangle.
    /// r2: Bottom Right coordinate of second rectangle.
    /// We need to write a function bool doOverlap(l1, r1, l2, r2) that returns true if the two given rectangles overlap.
    /// </summary>
    public class Question36 {
        public static void Init (string[] args) {
            Console.WriteLine (DoOverlap (new RectangleOverlapPoint (0, 10), new RectangleOverlapPoint (10, 0), new RectangleOverlapPoint (5, 5), new RectangleOverlapPoint (15, 0)));
        }

        private static bool DoOverlap (RectangleOverlapPoint l1, RectangleOverlapPoint r1, RectangleOverlapPoint l2, RectangleOverlapPoint r2) {
            bool result = true;

            if (l1.x > r2.x || l2.x > r1.x) {
                result = false;
            }

            if (l1.y < r2.y || l2.y < r1.y) {
                result = false;
            }

            return result;
        }
    }

    public class RectangleOverlapPoint {
        public int x { get; set; }
        public int y { get; set; }

        public RectangleOverlapPoint (int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}