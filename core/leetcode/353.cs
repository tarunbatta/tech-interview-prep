// Url: https://leetcode.com/problems/design-snake-game

/*
353. Design Snake Game
Medium

Design a Snake game that is played on a device with screen size height x width. Play the game online if you are not familiar with the game.

The snake is initially located at the top left corner (0, 0) with a length of 1 unit.

You are given an array food where food[i] = (r_i, c_i) is the row and column position of a piece of food that appears on the screen. The food array is given in the order that the food items appear.

Each time a piece of food is eaten, the snake's length and your score increase by 1, and the next food item appears on the screen.

The game is over if the snake hits a wall or collides with itself.

Implement the SnakeGame class:
- SnakeGame(int width, int height, int[][] food) Initializes the object with a screen of size width x height and the positions of the food.
- int move(String direction) Returns the score of the game after applying one direction move by the snake. If the game is over, return -1.

Example 1:
Input
["SnakeGame", "move", "move", "move", "move", "move", "move"]
[[3, 2, [[1, 2], [0, 1]]], ["R"], ["D"], ["R"], ["U"], ["L"], ["U"]]
Output
[null, 0, 0, 1, 1, 2, -1]

Constraints:
1 <= width, height <= 10^4
1 <= food.length <= 50
food[i].length == 2
0 <= r_i < height
0 <= c_i < width
direction is 'U', 'D', 'L', or 'R'.
At most 10^4 calls will be made to move.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem353
{
    public class Solution
    {
        public static void Init()
        {
            int[][] food =
            [
                [1, 2],
                [0, 1]
            ];

            SnakeGame snake = new(3, 2, food);
            Console.WriteLine(snake.Move("R")); // 0
            Console.WriteLine(snake.Move("D")); // 0
            Console.WriteLine(snake.Move("R")); // 1
            Console.WriteLine(snake.Move("U")); // 1
            Console.WriteLine(snake.Move("L")); // 2
            Console.WriteLine(snake.Move("U")); // -1
        }
    }

    // Time: O(1) per move using LinkedList and HashSet
    // Space: O(N) where N is snake length <= width * height
    public class SnakeGame
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int[][] _food;
        private int _foodIndex;
        private readonly LinkedList<(int r, int c)> _body;
        private readonly HashSet<(int r, int c)> _bodySet;

        public SnakeGame(int width, int height, int[][] food)
        {
            _width = width;
            _height = height;
            _food = food ?? [];
            _foodIndex = 0;

            _body = new LinkedList<(int r, int c)>();
            _bodySet = [];

            _body.AddFirst((0, 0));
            _bodySet.Add((0, 0));
        }

        public int Move(string direction)
        {
            var (headR, headC) = _body.First.Value;
            int newR = headR;
            int newC = headC;

            switch (direction)
            {
                case "U":
                    newR--;
                    break;
                case "D":
                    newR++;
                    break;
                case "L":
                    newC--;
                    break;
                case "R":
                    newC++;
                    break;
            }

            // Boundary collision
            if (newR < 0 || newR >= _height || newC < 0 || newC >= _width)
            {
                return -1;
            }

            // Food collision
            bool ateFood = _foodIndex < _food.Length && newR == _food[_foodIndex][0] && newC == _food[_foodIndex][1];

            if (ateFood)
            {
                _foodIndex++;
            }
            else
            {
                // Remove tail so head can move into previous tail space
                var tail = _body.Last.Value;
                _body.RemoveLast();
                _bodySet.Remove(tail);
            }

            // Self collision
            if (_bodySet.Contains((newR, newC)))
            {
                return -1;
            }

            _body.AddFirst((newR, newC));
            _bodySet.Add((newR, newC));

            return _body.Count - 1;
        }
    }
}