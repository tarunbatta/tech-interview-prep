/*
From repository root:
dotnet run --project src/dotnet/core

From the core directory:
cd src/dotnet/core
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