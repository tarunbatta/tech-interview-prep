/*
APP: There is an App for that
---------------------------------------
There are a few words to which you can insert the letters a,p,p to create a new word with a completely different meaning. The letters must be added in sequence but need not be continuous. For example, you can add “app” to “hen” to make “hAPPen” and to todies to make toAdPiPes.

Basically your goal is to find pairs of words from a given word list (https://github.com/dwyl/english-words/blob/master/words.txt) that differ by exactly 3 letters (and those letters are a, p & p) in sequence.
*/
(function() {
    /*
    Time Complexity: 
    Space Complexity: 
    */
    var appForThat = function(word1, word2) {
        var result = [];

        if (word1 != null && word1.length > 0 && word2 != null && word2.length > 0) {
            var dict = readTextFile('../../../shared/assets/words.txt');
            var filter1 = GetFilteredWords(word1, dict);
            result = GetFilteredWords(word2, filter1);
        }

        return result;
    };

    function IsWordPresent() {


    }

    function GetFilteredWords(word, dict) {
        var result = [];

        if (word != null && word.length > 0) {
            if (dict != null && dict.length > 0) {
                var regex = '.*';
                i = 0;

                while (i < word.length) {
                    regex += word[i] + '.*';
                    i++;
                }

                for (var i = 0; i < dict.length; i++) {
                    var found = dict[i].match(regex);

                    if (found != null) {
                        result.push(found.input);
                    }
                }
            }
        }

        return result;
    }

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

    console.log("null,null:", appForThat(null, null));
    console.log("undefined,undefined:", appForThat(undefined, undefined));
    console.log("app,happen:", appForThat('app', 'hen'));
    console.log("app,todies:", appForThat('app', 'todies'));
})();