/*
Given a time, calculate the angle between the hour and minute hands.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c0q2 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (DateTime.Now + ": " + CalculateAngle (DateTime.Now));
        }

        private static int CalculateAngle (DateTime time) {
            int result = 0;

            int minuteAngle = (360 * time.Minute) / 60;
            int hourAngle = 360 * (time.Hour % 12) / 12 + 360 * (time.Minute / 60) * (1 / 12);
            result = (hourAngle - minuteAngle) % 360;

            return result;
        }
    }
}