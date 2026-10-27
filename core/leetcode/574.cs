// Url: https://leetcode.com/problems/winning-candidate

/*
574. Winning Candidate
Medium

Table: Candidate
+-----+---------+
| id  | name    |
+-----+---------+
| int | varchar |
+-----+---------+
id is the primary key column for this table.
Each row of this table contains information about the id and the name of a candidate.

Table: Vote
+-----+--------------+
| id  | candidateId  |
+-----+--------------+
| int | int          |
+-----+--------------+
id is an auto-increment primary key.
candidateId is an id from the Candidate table.
Each row of this table contains information about one vote cast for a candidate.

Write a solution to report the name of the winning candidate (i.e., the candidate who got the largest number of votes).
The test cases are generated such that exactly one candidate wins the elections.

SQL Solution:
SELECT c.name
FROM Candidate c
JOIN Vote v ON c.id = v.candidateId
GROUP BY c.id, c.name
ORDER BY COUNT(v.id) DESC
LIMIT 1;
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem574
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Vote
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Candidate> candidates =
            [
                new Candidate { Id = 1, Name = "A" },
                new Candidate { Id = 2, Name = "B" },
                new Candidate { Id = 3, Name = "C" },
                new Candidate { Id = 4, Name = "D" },
                new Candidate { Id = 5, Name = "E" }
            ];

            List<Vote> votes =
            [
                new Vote { Id = 1, CandidateId = 2 },
                new Vote { Id = 2, CandidateId = 4 },
                new Vote { Id = 3, CandidateId = 3 },
                new Vote { Id = 4, CandidateId = 2 },
                new Vote { Id = 5, CandidateId = 5 }
            ];

            Console.WriteLine(FindWinningCandidate(candidates, votes)); // B
        }

        // Time: O(V + C) where V is votes.Count and C is candidates.Count
        // Space: O(C) for vote counts lookup
        public static string FindWinningCandidate(IList<Candidate> candidates, IList<Vote> votes)
        {
            if (candidates == null || votes == null || votes.Count == 0)
            {
                return string.Empty;
            }

            int winningCandidateId = votes
                .GroupBy(v => v.CandidateId)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;

            return candidates.FirstOrDefault(c => c.Id == winningCandidateId)?.Name ?? string.Empty;
        }
    }
}