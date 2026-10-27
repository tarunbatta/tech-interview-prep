/*
Arithmetic Path Problem
---------------------------------------
Given two numbers (both prime) print the path from the first prime number to the second. You can transform from one number to another as long as the intermediate numbers are all primes and have edit distance of 1.

Eg: 2, 13
Output: 2 > 3 > 13
*/
(function() {
    /*
    Time Complexity: 
    Space Complexity: 
    */
    var arithmeticPathProblem = function(num1, num2) {
        var result = null;

        if (num1 != null && num1 > 0 && num2 != null && num2 > 0) {
            // 1. get prime numbers by sieve
            // 2. create graph with edit distance of 1
            // 3. do bfs
        }

        return result;
    };

    console.log("null,null:", arithmeticPathProblem(null, null));
    console.log("undefined,undefined:", arithmeticPathProblem(undefined, undefined));
    console.log("2,13:", arithmeticPathProblem(2, 13));
})();