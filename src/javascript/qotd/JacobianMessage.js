/*
Jacobian Message
---------------------------------------
Jcb (actually Jacob), has a broken keyboard that has a few keys missing (specifically a,e,i,o,u and spacebar). If he wants to type a message like “I am happy” it shows up as “mhppy”.

Given the final string “mhppy”, return true if this is a Jacobian message.

Use the SCOWL wordlist here => http://wordlist.aspell.net/
*/
(function() {
    /*
    Time Complexity: 
    Space Complexity: 
    */
    var jacobianMessage = function(str) {
        var result = null;

        if (str != null && str.length > 0) {
            var dict = readTextFile('../words_scowl.txt');
            console.log(dict);

            // Process disct wihtout viwels
            // word break problem http://www.geeksforgeeks.org/word-break-problem-using-backtracking/
        }

        return result;
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

    console.log("null:", jacobianMessage(null));
    console.log("undefined:", jacobianMessage(undefined));
    console.log("mhppy:", jacobianMessage('mhppy'));
})();