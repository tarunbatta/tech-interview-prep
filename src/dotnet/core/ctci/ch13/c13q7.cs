/*
Lambda Expressions: 

There is a class Country that has the methods getContinent () and
getPopulation (). Write a function int getPopulation (List<Country> countries,
String continent) that computes the total population of a given continent.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.Ctci.c13q7 {
    public class Solution {
        public class Country {
            public string Continent { get; set; }
            public int Population { get; set; }
        }

        public static int GetPopulation (List<Country> countries, string continent) {
            return countries
                .Where (c => c.Continent == continent)
                .Sum (c => c.Population);
        }

        public static void Init (string[] args = null) {
            List<Country> countries = new List<Country> {
                new Country { Continent = "Asia", Population = 1000 },
                new Country { Continent = "Europe", Population = 500 },
                new Country { Continent = "Asia", Population = 1400 },
                new Country { Continent = "Africa", Population = 800 }
            };

            Console.WriteLine ($"Asia Population: {GetPopulation (countries, "Asia")}");
        }
    }
}
