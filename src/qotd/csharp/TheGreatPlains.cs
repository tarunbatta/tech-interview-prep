using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// The Great Plains
    ///
    /// Given a two dimensional array, each point representing the “Height” of a geographic area.
    /// Calculate the amount of water that will be retained by that area if it were to “rain” a lot.
    ///
    /// Input:
    /// [3,3,3]
    /// [3,1,3]
    /// [3,3,3]
    ///
    /// Output: 2, the middle cell will retain 2 units of water.
    ///
    /// Input:
    /// [3,3,3,3]
    /// [3,1,2,3]
    /// [3,3,3,3]
    ///
    /// Output: 3, the middle two cells will retain 2 units and 1 unit of water respectively.
    ///
    /// Input:
    /// [3,3,3,3,3]
    /// [3,2,2,2,3]
    /// [3,2,1,2,3]
    /// [3,2,2,2,3]
    /// [3,3,3,3,3]
    ///
    /// Output: 10, the middle two cells will retain 2 units and 1 unit of water respectively.
    ///
    /// Use least space, least time.
    /// </summary>
    public class TheGreatPlains {
        public static void Init () {
            Console.WriteLine (CalculateWaterInArea (null));
            Console.WriteLine (CalculateWaterInArea (new int[3, 3] { { 3, 3, 3 }, { 3, 1, 3 }, { 3, 3, 3 } }));
            Console.WriteLine (CalculateWaterInArea (new int[3, 4] { { 3, 3, 3, 3 }, { 3, 1, 2, 3 }, { 3, 3, 3, 3 } }));
            Console.WriteLine (CalculateWaterInArea (new int[5, 5] { { 3, 3, 3, 3, 3 }, { 3, 2, 2, 2, 3 }, { 3, 2, 1, 2, 3 }, { 3, 2, 2, 2, 3 }, { 3, 3, 3, 3, 3 } }));
            Console.WriteLine (CalculateWaterInArea (new int[4, 4] { { 3, 3, 3, 3 }, { 3, 2, 1, 1 }, { 3, 1, 3, 1 }, { 4, 5, 3, 6 } }));
        }

        public static int CalculateWaterInArea (int[, ] area) {
            int result = 0;

            if (area != null) {
                int rows = area.GetLength (0);
                int cols = area.GetLength (1);

                if (rows > 2 && cols > 2) {
                    int[, ] la = (int[, ]) area.Clone ();
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < rows; j++) {
                            la[i, j] = area[i, 0] - la[i, j];
                        }
                    }

                    int[, ] ta = (int[, ]) area.Clone ();
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < rows; j++) {
                            ta[i, j] = area[0, j] - ta[i, j];
                        }
                    }

                    int[, ] ra = (int[, ]) area.Clone ();
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < rows; j++) {
                            ra[i, j] = area[i, cols - 1] - ra[i, j];
                        }
                    }

                    int[, ] ba = (int[, ]) area.Clone ();
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < rows; j++) {
                            ba[i, j] = area[rows - 1, j] - ba[i, j];
                        }
                    }

                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < rows; j++) {
                            int left = (la[i, j] < 0 ? 0 : la[i, j]);
                            int top = (ta[i, j] < 0 ? 0 : ta[i, j]);
                            int right = (ra[i, j] < 0 ? 0 : ra[i, j]);
                            int bottom = (ba[i, j] < 0 ? 0 : ba[i, j]);

                            result += Math.Min (Math.Min (left, top), Math.Min (right, bottom));
                        }
                    }
                }
            }

            return result;
        }
    }
}