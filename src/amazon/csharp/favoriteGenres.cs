/*
Amazon OA: Favorite Genres

Given a map Map<String, List<String>> userSongs with user names as keys and a list of all the songs that the user
has listened to as values.
Also given a map Map<String, List<String>> songGenres, with song genre as keys and a list of all the songs within
that genre as values. The song can only belong to only one genre.
The task is to return a map Map<String, List<String>>, where the key is a user name and the value is a list of the
user's favorite genre(s). Favorite genre is the most listened to genre. A user can have more than one favorite genre
if he/she has listened to the same number of songs per each of the genres.

Example:
userSongs = {
   "David": ["song1", "song2", "song3", "song4", "song8"],
   "Emma":  ["song5", "song6", "song7"]
}
songGenres = {
   "Rock":    ["song1", "song3"],
   "Dubstep": ["song7"],
   "Techno":  ["song2", "song4"],
   "Pop":     ["song5", "song6"],
   "Jazz":    ["song8", "song9"]
}
Output: {
   "David": ["Rock", "Techno"],
   "Emma":  ["Pop"]
}
If a user's songs belong to no known genre, the user maps to an empty list.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Amazon {
    public class FavoriteGenres {
        public static void Init () {
            Dictionary<string, List<string>> userSongs = new () {
                ["David"] = ["song1", "song2", "song3", "song4", "song8"],
                ["Emma"] = ["song5", "song6", "song7"],
                ["Nobody"] = ["song42"],
            };

            Dictionary<string, List<string>> songGenres = new () {
                ["Rock"] = ["song1", "song3"],
                ["Dubstep"] = ["song7"],
                ["Techno"] = ["song2", "song4"],
                ["Pop"] = ["song5", "song6"],
                ["Jazz"] = ["song8", "song9"],
            };

            foreach (var kv in Solve (userSongs, songGenres)) {
                Console.WriteLine ($"{kv.Key}: [{string.Join (", ", kv.Value)}]");
            }
            // David: [Rock, Techno]
            // Emma: [Pop]
            // Nobody: []
        }

        // Invert songGenres to song -> genre, then count per user.
        // Time: O (S + U), S = total songs across genres, U = total songs across users
        // Space: O (S)
        public static Dictionary<string, List<string>> Solve (
            Dictionary<string, List<string>> userSongs,
            Dictionary<string, List<string>> songGenres) {

            Dictionary<string, string> songToGenre = new Dictionary<string, string> ();
            foreach (var kv in songGenres) {
                foreach (string song in kv.Value) {
                    songToGenre[song] = kv.Key;
                }
            }

            Dictionary<string, List<string>> result = new Dictionary<string, List<string>> ();

            foreach (var kv in userSongs) {
                Dictionary<string, int> counts = new Dictionary<string, int> ();
                int max = 0;

                foreach (string song in kv.Value) {
                    if (songToGenre.TryGetValue (song, out string genre)) {
                        counts[genre] = counts.GetValueOrDefault (genre) + 1;
                        max = Math.Max (max, counts[genre]);
                    }
                }

                List<string> favorites = new List<string> ();
                foreach (var c in counts) {
                    if (c.Value == max) {
                        favorites.Add (c.Key);
                    }
                }

                result[kv.Key] = favorites;
            }

            return result;
        }
    }
}
