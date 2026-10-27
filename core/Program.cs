/*
You can now run it from either location:

From repository root:

bash
dotnet run --project core
From the core directory:

bash
cd core
dotnet run
*/

using System;
using InterviewPreperationGuide.Core;

namespace InterviewPreperationGuide.Core {
    class Program {
        static void Main (string[] args) {
            //Algorithms.Search.Dijkstra.Init ();
            //Algorithms.Others.LongestCommonSubstring.Init();

            LeetCode.problem2384.Solution.Init();
        }
    }
}