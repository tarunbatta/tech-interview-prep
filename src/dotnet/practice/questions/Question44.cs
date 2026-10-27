using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// The Stock Span Problem
    /// The stock span problem is a financial problem where we have a series of n daily price quotes
    /// for a stock and we need to calculate span of stock’s price for all n days.
    ///
    /// The span Si of the stock’s price on a given day i is defined as the maximum number of consecutive days
    /// just before the given day, for which the price of the stock on the current day is less than or
    /// equal to its price on the given day.
    ///
    /// For example, if an array of 7 days prices is given as {100, 80, 60, 70, 60, 75, 85},
    /// then the span values for corresponding 7 days are {1, 1, 1, 2, 1, 4, 6}
    /// </summary>
    public class Question44 {
        public static void Init (string[] args) {
            CalculateSpan_a (null);
            
            CalculateSpan_a (new int[] { 100 });
            
            CalculateSpan_a (new int[] { 100, 80, 60, 70, 60, 75, 85 });
            
            CalculateSpan_a (new int[] { 59, 60, 61, 59, 62, 59, 60, 61, 62 });
            
            CalculateSpan_a (new int[] { 59, 60, 59, 59, 57, 65, 61, 62 });

            

            CalculateSpan_b (null);
            
            CalculateSpan_b (new int[] { 100 });
            
            CalculateSpan_b (new int[] { 100, 80, 60, 70, 60, 75, 85 });
            
            CalculateSpan_b (new int[] { 59, 60, 61, 59, 62, 59, 60, 61, 62 });
            
            CalculateSpan_b (new int[] { 59, 60, 59, 59, 57, 65, 61, 62 });
        }

        public static void CalculateSpan_a (int[] price) {
            if (price != null && price.Length > 0) {
                int[] result = new int[price.Length];

                result[0] = 1;

                for (int i = 1; i < price.Length; i++) {
                    result[i] = 1;

                    for (int j = i - 1; j >= 0 && price[i] >= price[j]; j--) {
                        result[i]++;
                    }
                }

                Console.WriteLine (string.Join (",", result));
            } else {
                Console.WriteLine ("No items present.");
            }
        }

        public static void CalculateSpan_b (int[] price) {
            if (price != null && price.Length > 0) {
                int[] result = new int[price.Length];
                Stack<int> s = new Stack<int> ();

                result[0] = 1;
                s.Push (0);

                for (int i = 1; i < price.Length; i++) {
                    // Pop elements from stack while stack is not empty and top of stack is smaller than price[i]
                    while (s.Count > 0 && price[s.Peek ()] <= price[i]) {
                        s.Pop ();
                    }

                    // If stack becomes empty, then price[i] is greater than all elements
                    // on left of it, i.e., price[0], price[1],..price[i-1].  Else price[i]
                    // is greater than elements after top of stack
                    result[i] = (s.Count == 0) ? i + 1 : (i - s.Peek ());

                    // Push this element to stack
                    s.Push (i);
                }

                Console.WriteLine (string.Join (",", result));
            } else {
                Console.WriteLine ("No items present.");
            }
        }
    }
}