using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Fair Bonuses:
    /// You manage a team of developers and you have to give concert tickets as a bonus to the developers.
    /// For each developer, you know how many lines of code he/she wrote the previous week,
    /// and you want to reward more productive developers.
    /// The developers sit in a row. Each developer, except the first and last, has two neighbors.
    /// You must give each developer one or more tickets in such a way that if a developer has
    /// written more lines of code than a neighbor,
    /// then he/she receives more tickets than his/her neighbor.
    /// Compute the minimum number of tickets you need to buy to satisfy the constraint.
    /// For example, if Andy, Bob, Charlie and David sit in a row from left to right,
    /// and the previous week they wrote 300, 400, 500, and 200 lines of code respectively,
    /// then Andy and David should receive 1 ticket each, Bob should receive 2 tickets,
    /// and Charlie should receive 3 tickets, for a total of 7 tickets.
    /// </summary>
    public class FairBonuses {
        public static void Init (string[] args) {
            /*
			Tickets: null
			Total: 0
			Tickets: 300, 400, 500, 200
			Total: 7
			Tickets: 11, 10, 10, 10, 15, 18, 18, 18
			Total: 16
			Tickets: 10, 9, 8, 7, 6, 5, 9, 4, 3, 2, 1
			Total: 36
			Tickets: 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 50
			Total: 66
			Tickets: 800, 700, 600, 1000, 500, 400, 2000, 200, 3000
			Total: 17
			Tickets: 1000, 900, 800, 700, 500, 600, 700, 800
			Total: 24
			Tickets: 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
			Total: 66
			Tickets: 3, 2, 1, 3, 2, 1, 2, 1, 2
			Total: 17
			Tickets: 5, 4, 3, 2, 1, 2, 3, 4
			Total: 24
			Tickets: 1, 1, 1, 1, 1, 1
			Total: 6
			Tickets: 100, 50, 200, 200, 200, 10, 20, 200, 100, 100, 100
			Total: 18
			*/

            GetTotalTickets (null);
            
            GetTotalTickets (new int[] { 300, 400, 500, 200 });
            
            GetTotalTickets (new int[] { 11, 10, 10, 10, 15, 18, 18, 18 });
            
            GetTotalTickets (new int[] { 10, 9, 8, 7, 6, 5, 9, 4, 3, 2, 1 });
            
            GetTotalTickets (new int[] { 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 50 });
            
            GetTotalTickets (new int[] { 800, 700, 600, 1000, 500, 400, 2000, 200, 3000 });
            
            GetTotalTickets (new int[] { 1000, 900, 800, 700, 500, 600, 700, 800 });
            
            GetTotalTickets (new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 });
            
            GetTotalTickets (new int[] { 3, 2, 1, 3, 2, 1, 2, 1, 2 });
            
            GetTotalTickets (new int[] { 5, 4, 3, 2, 1, 2, 3, 4 });
            
            GetTotalTickets (new int[] { 1, 1, 1, 1, 1, 1 });
            
            GetTotalTickets (new int[] { 100, 50, 200, 200, 200, 10, 20, 200, 100, 100, 100 });
        }

        private static void GetTotalTickets (int[] arr) {
            int result = 0;
            int defaultTicket = 1;

            if (arr != null && arr.Length > 0) {
                Dictionary<int, int> dict = new Dictionary<int, int> ();
                for (int i = 0; i < arr.Length; i++) {
                    dict.Add (i, defaultTicket);
                }

                for (int i = 1; i < arr.Length; i++) {
                    if (arr[i] > arr[i - 1]) {
                        dict[i] = dict[i - 1] + 1;
                    } else if (arr[i] == arr[i - 1]) {
                        dict[i] = dict[i - 1];
                    } else {
                        int j = i;
                        while (j - 1 >= 0 && arr[j - 1] >= arr[j] && dict[j - 1] <= dict[j]) {
                            if (arr[j - 1] == arr[j]) {
                                dict[j - 1] = dict[j];
                            } else {
                                dict[j - 1] = dict[j] + 1;
                            }

                            j--;
                        }
                    }
                }

                foreach (var item in dict) {
                    result += item.Value;
                }
            }

            Console.WriteLine ("Total Number of Tickets: " + result);
        }
    }
}