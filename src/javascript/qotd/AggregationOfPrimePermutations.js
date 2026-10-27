/*
APP: Aggregation of Prime Permutations
---------------------------------------
Given an input number, return the sum of all permutations of the number that are prime. You do not have to use all digits in your permutation.

Input: 123
Output: 72
Explanation: Total of (2 + 3 + 13 + 23 + 31)
*/
(function() {
    var permutations = new Array();

    /*
    Time Complexity: 
    Space Complexity: 
    */
    var aggregationOfPrimePermutations = function(num) {
        var result = 0;

        if (num != null && num > 0) {
            var digits = num.toString().split('').map(function(t) {
                return parseInt(t);
            });

            GetPermutations('', num.toString());
            var arr = permutations;

            for (var i = 0; i < arr.length; i++) {
                if (IsPrime(parseInt(arr[i]))) {
                    result += parseInt(arr[i]);
                }
            }
        }

        return result;
    };

    function GetPermutations(prefix, num) {
        var n = num.length;

        if (n == 0) {
            permutations.push(parseInt(prefix));
        } else {
            for (var i = n; i > 0; i--) {
                for (var j = 0; j < i; j++) {
                    var prefix_new = prefix + num.charAt(j);
                    var num_new = num.substring(0, j) + num.substring(j + 1, i);
                    GetPermutations(prefix_new, num_new);
                }
            }
        }
    }

    function DigitsToNumber(digits) {
        var result = 0;

        if (digits != null && digits.length > 0) {
            result = parseInt(digits.join(''));
        }

        return result;
    }

    function Swap(arr, i, j) {
        if (arr != null && arr.length > 0 && i < arr.length && j < arr.length) {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    function IsPrime(num) {
        if (num <= 1) {
            return false;
        } else {
            for (var i = 2; i * i < num; i++) {
                if (num % i) {
                    return false;
                }
            }
        }

        return true;
    }

    console.log("null:", aggregationOfPrimePermutations(null));
    console.log("undefined:", aggregationOfPrimePermutations(undefined));
    console.log("0:", aggregationOfPrimePermutations(0));
    console.log("123:", aggregationOfPrimePermutations(123));
})();