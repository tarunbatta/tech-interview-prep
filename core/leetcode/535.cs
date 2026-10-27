// Url:https://leetcode.com/problems/encode-and-decode-tinyurl

/*
535. Encode and Decode TinyURL
Medium

Note: This is a companion problem to the System Design problem: Design TinyURL.

TinyURL is a URL shortening service where you enter a URL such as https://leetcode.com/problems/design-tinyurl and it returns a short URL such as http://tinyurl.com/4e9iAk.

Design the encode and decode methods for the TinyURL service. There is no restriction on how your encode/decode algorithm should work. You just need to ensure that a URL can be encoded to a tiny URL and the tiny URL can be decoded to the original URL.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem535 {
    public class Solution {
        public static void Init () {
            Codec codec = new();
            codec.decode (codec.encode ("https://leetcode.com/problems/design-tinyurl"));
        }
    }

    public class Codec {
        string _chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string _urlPrefix = "http://tinyurl.com/";
        const int _maxKeyLength = 6;
        Random random = new();
        Dictionary<string, string> _dict = [];

        private string getRandomKey () {
            StringBuilder sb = new();

            for (int i = 0; i < _maxKeyLength; i++) {
                sb.Append (_chars[random.Next (_chars.Length)]);
            }

            return sb.ToString ();
        }

        // Encodes a URL to a shortened URL    
        public string encode (string longUrl) {
            string key = null;

            while (key == null || _dict.ContainsKey (key)) {
                key = getRandomKey ();
            }

            _dict.Add (key, longUrl);

            return _urlPrefix + key;
        }

        // Decodes a shortened URL to its original URL.    
        public string decode (string shortUrl) {
            string result = null;
            string key = "";

            if (!string.IsNullOrEmpty (shortUrl)) {
                key = shortUrl.Substring (shortUrl.IndexOf (_urlPrefix) + _urlPrefix.Length, shortUrl.Length - _urlPrefix.Length);
            }

            if (!string.IsNullOrEmpty (key) && _dict.ContainsKey (key)) {
                result = _dict[key];
            }

            return result;
        }
    }
}