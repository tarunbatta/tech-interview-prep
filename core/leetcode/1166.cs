namespace InterviewPreperationGuide.Core.LeetCode.problem1166
{
    using System;
    using System.Collections.Generic;

    // 1166. Design File System
    // https://leetcode.com/problems/design-file-system/
    //
    // You are asked to design a file system that allows you to create new paths and associate them with different values.
    //
    // The format of a path is one or more concatenated strings of the form: / followed by one or more lowercase English letters.
    // For example, "/leetcode" and "/leetcode/problems" are valid paths while an empty string "" and "/" are not.
    //
    // Implement the FileSystem class:
    // - bool CreatePath(string path, int value) Creates a new path and associates a value to it if possible and returns true.
    //   Returns false if the path already exists or its parent path doesn't exist.
    // - int Get(string path) Returns the value associated with path or returns -1 if the path doesn't exist.
    //
    // Example 1:
    // Input: 
    // ["FileSystem","createPath","get"]
    // [[],["/a",1],["/a"]]
    // Output: 
    // [null,true,1]
    //
    // Example 2:
    // Input: 
    // ["FileSystem","createPath","createPath","get","createPath","get"]
    // [[],["/leet",1],["/leet/code",2],["/leet/code"],["/c/d",1],["/c"]]
    // Output: 
    // [null,true,true,2,false,-1]
    //
    // Constraints:
    // 2 <= path.length <= 100
    // 1 <= value <= 10^9
    // Each path consists of lowercase English letters and '/'.
    // At most 10^4 calls in total will be made to createPath and get.
    public class FileSystem
    {
        private readonly Dictionary<string, int> paths;

        public FileSystem()
        {
            paths = new Dictionary<string, int>();
        }

        // Time: O(L) where L is path length
        // Space: O(L)
        public bool CreatePath(string path, int value)
        {
            if (string.IsNullOrEmpty(path) || path == "/" || paths.ContainsKey(path))
            {
                return false;
            }

            int lastSlash = path.LastIndexOf('/');
            if (lastSlash < 0)
            {
                return false;
            }

            string parent = path.Substring(0, lastSlash);
            if (parent.Length > 0 && !paths.ContainsKey(parent))
            {
                return false;
            }

            paths[path] = value;
            return true;
        }

        // Time: O(L) where L is path length
        // Space: O(1)
        public int Get(string path)
        {
            return paths.TryGetValue(path, out int val) ? val : -1;
        }
    }

    public class Solution
    {
        public void Init()
        {
            var fs1 = new FileSystem();
            Console.WriteLine(fs1.CreatePath("/a", 1)); // true
            Console.WriteLine(fs1.Get("/a")); // 1

            var fs2 = new FileSystem();
            Console.WriteLine(fs2.CreatePath("/leet", 1)); // true
            Console.WriteLine(fs2.CreatePath("/leet/code", 2)); // true
            Console.WriteLine(fs2.Get("/leet/code")); // 2
            Console.WriteLine(fs2.CreatePath("/c/d", 1)); // false
            Console.WriteLine(fs2.Get("/c")); // -1
        }
    }
}