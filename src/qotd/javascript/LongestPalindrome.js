/*
Make Longest Palindrome
---------------------------------------
Given a string, find the longest palindrome that you can make by removing letters from it.
Input: google
Output: 4, goog // By deleting l & e

Input: madman
Output: 3, ada // or 
Output: 3, mdm
*/
(function() {
    /*
    Time Complexity: O(n/2)
    Space Complexity: O(n)
    */
    var longestPalindrome = function(str) {
        var result = null;

        // TODO
        //http://www.geeksforgeeks.org/dynamic-programming-set-12-longest-palindromic-subsequence/

        //   g o o g l e
        // g 1 1 2 4 
        // o   1
        // o     1
        // g       1
        // l         1
        // e           1

        // p(i,j)  = 0 where j<i 

        // p(i,j) = if (a[i] == a[j]) p(i+1, j-1) + 2
        //              Max(p(i+1,j), p(i, j-1))

        if (str != null) {
            if (str.length > 1) {
                var frequency = GetCharFrequencyInString(str);
                var validPalindroms = [];
                longestPalindromeHelper(str, validPalindroms);

                result = validPalindroms.reduce(function(a, b) {
                    return a.length > b.length ? a : b;
                });
            } else {
                result = '';
            }
        }

        return result;
    };

    function longestPalindromeHelper(str, validPalindroms) {
        var result = '';
        var beg = '';
        var end = '';
        var i = 0;
        var j = str.length - 1;
        var frequency = GetCharFrequencyInString(str);

        while (i <= j) {
            if (str[i] == str[j] && (i != j)) {
                beg += str[i];
                end = str[j] + end;

                frequency[str[i].charCodeAt(0)]--;
                frequency[str[j].charCodeAt(0)]--;
                i++;
                j--;
            } else {
                if (frequency[str[i].charCodeAt(0)] > 1 && frequency[str[j].charCodeAt(0)] > 1) {
                    longestPalindromeHelper(str.substring(i + 1, j + 1), validPalindroms);
                    longestPalindromeHelper(str.substring(i, j), validPalindroms);
                    return;
                } else if (frequency[str[i].charCodeAt(0)] > 1) {
                    frequency[str[j].charCodeAt(0)]--;
                    j--;
                } else if (frequency[str[j].charCodeAt(0)] > 1) {
                    frequency[str[i].charCodeAt(0)]--;
                    i++;
                } else {
                    if (i == j || (j - i == 1)) {
                        beg += str[i];
                        frequency[str[i].charCodeAt(0)]--;
                        frequency[str[j].charCodeAt(0)]--;
                        i++;
                        j--;
                    } else {
                        frequency[str[i].charCodeAt(0)]--;
                        frequency[str[j].charCodeAt(0)]--;
                        i++;
                        j--;
                    }
                }
            }
        }

        result = beg + end;
        validPalindroms.push(result);

        return result;
    }

    function GetCharFrequencyInString(str) {
        var result = new Array(256);

        for (var i = 0; i < result.length; i++) {
            result[i] = 0;
        }

        if (str != null && str.length > 0) {
            for (var i = 0; i < str.length; i++) {
                var letter = str.charAt(i);
                var letterCode = letter.charCodeAt(0);
                result[letterCode]++;
            }
        }

        return result;
    }

    console.log("null:", longestPalindrome(null));
    console.log("undefined:", longestPalindrome(undefined));
    console.log("a:", longestPalindrome('a'));
    console.log("google:", longestPalindrome('google'));
    console.log("googogle:", longestPalindrome('googogle'));
    console.log("madman:", longestPalindrome('madman'));
    console.log("madam:", longestPalindrome('madam'));
    console.log("undefined:", longestPalindrome('undefined'));
    console.log("abbbcdca:", longestPalindrome('abbbcdca'));
})();