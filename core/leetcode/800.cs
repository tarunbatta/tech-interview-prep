// Url:https://leetcode.com/problems/similar-rgb-color

/*
800. Similar RGB Color
Easy

The red-green-blue color "#AABBCC" can be written as "#ABC" in shorthand.
- For example, "#15c" is shorthand for the color "#1155cc".

The similarity between the two colors "#ABCDEF" and "#UVWXYZ" is -((AB - UV)^2 + (CD - WX)^2 + (EF - YZ)^2).

Given a 7-character color string color, return a 7-character color that is most similar to the given color and has a shorthand (that is, it can be represented as some "#XYZ").

Any answer that has the same highest similarity as the best answer will be accepted.

Example 1:
Input: color = "#09f166"
Output: "#11ee66"
Explanation: 
The similarity is -(0x09 - 0x11)^2 -(0xf1 - 0xee)^2 - (0x66 - 0x66)^2 = -64 -9 -0 = -73.
This is the highest among any shaded color, including (for example) "#00ee66", which has similarity -116.

Example 2:
Input: color = "#4e3fe1"
Output: "#5544dd"

Constraints:
- color.length == 7
- color[0] == '#'
- color[i] is a hexadecimal digit for all i > 0.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem800 {
    public class Solution {
        public void Init() {
            Console.WriteLine(SimilarRGB("#09f166")); // "#11ee66"
            Console.WriteLine(SimilarRGB("#4e3fe1")); // "#5544dd"
        }

        // Time: O(1)
        // Space: O(1)
        public string SimilarRGB(string color) {
            StringBuilder sb = new("#");
            for (int i = 1; i < 7; i += 2) {
                int val = Convert.ToInt32(color.Substring(i, 2), 16);
                int k = (val + 8) / 17;
                string hexDigit = k.ToString("x");
                sb.Append(hexDigit).Append(hexDigit);
            }
            return sb.ToString();
        }
    }
}