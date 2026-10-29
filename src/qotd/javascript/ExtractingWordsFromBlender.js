/*
Extracting Words from a Blender
---------------------------------------
Given a string like “SSDKILELS”, extract the source words that were merged to form this string. Example [SDE, SKILLS].

Assumptions
- You are given a dictionary ( use http://wordlist.aspell.net/ ).
- There may be “multiple” words blended in.
- Letter is the word must be in the same order in the blended string and the original words.
*/
(function() {
    /*
    Time Complexity: 
    Space Complexity: 
    */
    var extractingWordsFromBlender = function(str) {
        var result = [];

        if (str != null && str.length > 0) {
            var trie = new Trie();
            var dict = readTextFile('../../../shared/assets/words.txt');

            for (var i = 0; i < dict.length; i++) {
                trie.add(dict[i]);
            }


        }

        return result;
    };

    function Node(data) {
        this.data = data;
        this.isWord = false;
        this.prefixes = 0;
        this.children = {};
    }

    function Trie() {
        this.root = new Node('');
    }

    Trie.prototype.add = function(word) {
        if (!this.root) {
            return null;
        }
        this._addNode(this.root, word);
    };

    Trie.prototype._addNode = function(node, word) {
        if (!node || !word) {
            return null;
        }
        node.prefixes++;
        var letter = word.charAt(0);
        var child = node.children[letter];
        if (!child) {
            child = new Node(letter);
            node.children[letter] = child;
        }
        var remainder = word.substring(1);
        if (!remainder) {
            child.isWord = true;
        }
        this._addNode(child, remainder);
    };

    Trie.prototype.contains = function(word) {
        if (!this.root) {
            return false;
        }
        return this._contains(this.root, word);
    };

    Trie.prototype._contains = function(node, word) {
        if (!node || !word) {
            return false;
        }
        var letter = word.charAt(0);
        var child = node.children[letter];
        if (child) {
            var remainder = word.substring(1);
            if (!remainder && child.isWord) {
                return true;
            } else {
                return this._contains(child, remainder);
            }
        } else {
            return false;
        }
    };

    function readTextFile(file) {
        var result = [];
        var fs = require('fs');

        if (fs != null) {
            var dataStream = fs.readFileSync(file);

            if (dataStream != null) {
                var dataString = dataStream.toString();

                if (dataString != null && dataString.length > 0) {
                    result = dataString.split('\n');
                }
            }
        }

        return result;
    }

    console.log("null:", extractingWordsFromBlender(null));
    console.log("undefined:", extractingWordsFromBlender(undefined));
    console.log("SSDKILELS:", extractingWordsFromBlender('SSDKILELS'));
    console.log("ATEA:", extractingWordsFromBlender('ATEA'));
})();