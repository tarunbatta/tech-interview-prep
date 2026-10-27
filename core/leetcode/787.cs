using System.Linq;
// Url:https://leetcode.com/problems/cheapest-flights-within-k-stops

/*
787. Cheapest Flights Within K Stops
Medium

There are n cities connected by m flights. Each fight starts from city u and arrives at v with a price w.

Now given all the cities and flights, together with starting city src and the destination dst, your task is to find the cheapest price from src to dst with up to k stops. If there is no such route, output -1.

Example 1:
Input: 
n = 3, edges = [[0,1,100],[1,2,100],[0,2,500]]
src = 0, dst = 2, k = 1
Output: 200
Explanation: 
The graph looks like this:

The cheapest price from city 0 to city 2 with at most 1 stop costs 200, as marked red in the picture.

Example 2:
Input: 
n = 3, edges = [[0,1,100],[1,2,100],[0,2,500]]
src = 0, dst = 2, k = 0
Output: 500
Explanation: 
The graph looks like this:

The cheapest price from city 0 to city 2 with at most 0 stop costs 500, as marked blue in the picture.

Note:
	The number of nodes n will be in range [1, 100], with nodes labeled from 0 to n - 1.
	The size of flights will be in range [0, n * (n - 1) / 2].
	The format of each flight will be (src, dst, price).
	The price of each flight will be in the range [1, 10000].
	k is in the range of [0, n - 1].
	There will not be any duplicated flights or self cycles.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem787 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (FindCheapestPrice(3, null, 0, 2, 1));
            Console.WriteLine (FindCheapestPrice(3, [], 0, 2, 1));
            Console.WriteLine (FindCheapestPrice(3, [
                [0, 1, 100],
                    [1, 2, 100],
                    [0, 2, 500]
            ], 0, 2, 1));
            Console.WriteLine (FindCheapestPrice(3, [
                [0, 1, 100],
                    [1, 2, 100],
                    [0, 2, 500]
            ], 0, 2, 0));
        }

        public static int FindCheapestPrice (int n, int[][] flights, int src, int dst, int K) {
            if (flights == null) {
                return -1;
            }

            List<Flight> flightsList = [];
            foreach (int[] flight in flights) {
                flightsList.Add (new Flight (flight[0], flight[1], flight[2]));
            }

            Queue<Destination> destinations = new Queue<Destination> ();
            destinations.Enqueue (new Destination (src, 0, 0));

            int[] tripCosts = new int[n];
            Array.Fill (tripCosts, -1);
            tripCosts[src] = 0;

            while (destinations.Count > 0) {
                Destination d = destinations.Dequeue ();
                List<Flight> f = flightsList.FindAll (x => x.source == d.city);

                if (d.stops > K || f.Count == 0) {
                    continue;
                }

                foreach (Flight item in f) {
                    int cost = d.cost + item.cost;

                    if (tripCosts[item.destination] == -1 || cost < tripCosts[item.destination]) {
                        tripCosts[item.destination] = cost;
                        destinations.Enqueue (new Destination (item.destination, cost, d.stops + 1));
                    }
                }
            }

            return tripCosts[dst];
        }

        public class Flight {
            public int source;
            public int destination;
            public int cost;

            public Flight (int source, int destination, int cost) {
                this.source = source;
                this.destination = destination;
                this.cost = cost;
            }
        }

        public class Destination {
            public int city;
            public int cost;
            public int stops;

            public Destination (int city, int cost, int stops) {
                this.city = city;
                this.cost = cost;
                this.stops = stops;
            }
        }
    }
}