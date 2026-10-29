/*
Extracting Primes from a sequence of digits
---------------------------------------
You are given a string of digits (upto 1000 chars). Your task is to split the string into prime numbers (2 .. 1million) such that the total is minimum. If you cannot split it in any way then return -1.
Ex: Input: 37347
Output: 60 => 3 + 7 + 3 + 47
Can also be 373 + 347, but that is larger.

PS: You have to use all digits.
*/
(function() {
    var min = [];

    /*
    Time Complexity: 
    Space Complexity: 
    */
    var extractingPrimesFromDigits = function(str) {
        var result = -1;

        if (str != null && str.length > 0) {
            min.push(-1);
            extractingPrimesFromDigitsHelper(str, 0, 0);

            result = min[0];
        }

        return result;
    };

    function extractingPrimesFromDigitsHelper(s, i, t) {
        if (i == s.length) {
            if (min[0] == -1 || t < min[0]) min[0] = t;
            return;
        }

        for (var j = i; j < s.length; j++) {
            var v = parseInt(s.substring(i, j + 1));

            if (IsPrime(v)) {
                extractingPrimesFromDigitsHelper(s, j + 1, t + v);
            }
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

    //console.log("null:", extractingPrimesFromDigits(null));
    //console.log("undefined:", extractingPrimesFromDigits(undefined));
    console.log("37347:", extractingPrimesFromDigits('37347'));
})();