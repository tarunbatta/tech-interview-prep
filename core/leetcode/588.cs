// Url:https://leetcode.com/problems/design-in-memory-file-system

/*
588. Design In-Memory File System
Hard

Design a data structure that simulates an in-memory file system.

Implement the FileSystem class:
- FileSystem() Initializes the object of the system.
- IList<string> Ls(string path)
  - If path is a file path, returns a list that only contains this file's name.
  - If path is a directory path, returns the list of file and directory names in this directory in lexicographical order.
- void Mkdir(string path) Makes a new directory according to the given path. The given directory path does not exist. If the middle directories in the path do not exist, you should create them as well.
- void AddContentToFile(string filePath, string content)
  - If filePath does not exist, creates that file containing given content.
  - If filePath already exists, appends the given content to original content.
- string ReadContentFromFile(string filePath) Returns the content in the file at filePath.

Example 1:
Input
["FileSystem", "ls", "mkdir", "addContentToFile", "ls", "readContentFromFile"]
[[], ["/"], ["/a/b/c"], ["/a/b/c/d", "hello"], ["/"], ["/a/b/c/d"]]
Output
[null, [], null, null, ["a"], "hello"]

Constraints:
- 1 <= path.length, filePath.length <= 100
- path and filePath are absolute paths which begin with '/' and do not end with '/' except that the path is just "/".
- You can assume that all directory names and file names only contain lowercase letters, and an open parenthesis '(' and a close parenthesis ')' will not appear in the names.
- 1 <= content.length <= 50
- At most 300 calls will be made to ls, mkdir, addContentToFile, and readContentFromFile.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem588 {
    public class FileSystem {
        private class FileNode {
            public bool IsFile { get; set; }
            public StringBuilder Content { get; } = new();
            public SortedDictionary<string, FileNode> Children { get; } = [];
        }

        private readonly FileNode _root = new();

        public FileSystem() { }

        // Time: O(k + m log m) where k is path depth, m is number of child entries
        // Space: O(m) for output list
        public IList<string> Ls(string path) {
            FileNode curr = _root;
            string[] parts = SplitPath(path);

            if (parts.Length > 0) {
                foreach (string part in parts) {
                    curr = curr.Children[part];
                }
                if (curr.IsFile) {
                    return [parts[^1]];
                }
            }

            return curr.Children.Keys.ToList();
        }

        // Time: O(k) where k is path depth
        // Space: O(k) for created nodes
        public void Mkdir(string path) {
            GetOrCreateNode(path, isFile: false);
        }

        // Time: O(k + content.Length)
        // Space: O(content.Length)
        public void AddContentToFile(string filePath, string content) {
            FileNode node = GetOrCreateNode(filePath, isFile: true);
            node.Content.Append(content);
        }

        // Time: O(k + content.Length)
        // Space: O(content.Length)
        public string ReadContentFromFile(string filePath) {
            FileNode curr = _root;
            foreach (string part in SplitPath(filePath)) {
                curr = curr.Children[part];
            }
            return curr.Content.ToString();
        }

        private FileNode GetOrCreateNode(string path, bool isFile) {
            FileNode curr = _root;
            string[] parts = SplitPath(path);

            for (int i = 0; i < parts.Length; i++) {
                string part = parts[i];
                if (!curr.Children.TryGetValue(part, out var next)) {
                    next = new FileNode();
                    curr.Children[part] = next;
                }
                curr = next;
            }

            if (isFile) {
                curr.IsFile = true;
            }

            return curr;
        }

        private static string[] SplitPath(string path) {
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class Solution {
        public void Init() {
            FileSystem fs = new();
            Console.WriteLine(string.Join(", ", fs.Ls("/"))); // []
            fs.Mkdir("/a/b/c");
            fs.AddContentToFile("/a/b/c/d", "hello");
            Console.WriteLine(string.Join(", ", fs.Ls("/"))); // "a"
            Console.WriteLine(fs.ReadContentFromFile("/a/b/c/d")); // "hello"
        }
    }
}