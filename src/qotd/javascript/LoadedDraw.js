/*
Building a Loaded Draw
---------------------------------------
You are given a list of Players (1-n). You are given the data set that captures who wins aginst who in a head to head matchup. Can you set up a tournament tree so that you can make player X (input) win?
4 players => A B C D
A > B
B > C
C > A  
A > D

Make A win
Answer YES: So set up first rount between A/D and B/C, A will win and B will win. Second round is A/B, and A will win.

Make C win
Answer YES: So set up A/D (winner A), setup A/B winner A , setup A/C winner C.
*/
(function() {
    /*
    Time Complexity: 
    Space Complexity: 
    */
    var loadedDraw = function() {
        var result = null;

        return result;
    };

    console.log("null:", loadedDraw(null));
    console.log("undefined:", loadedDraw(undefined));
})();