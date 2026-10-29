/*
From repository root:
dotnet run --project src/leetcode/csharp

From the leetcode directory:
cd src/leetcode/csharp
dotnet run
*/

using System;
using InterviewPreperationGuide.Core;

namespace InterviewPreperationGuide.Core {
    class Program {
        static void Main (string[] args) {
            //Algorithms.Search.Dijkstra.Init ();
            //Algorithms.Others.LongestCommonSubstring.Init();
            //Ctci.c1q1.Solution.Init();
            //Ctci.c4q8.Solution.Init();

            LeetCode.problem2384.Solution.Init();
        }
    }
}