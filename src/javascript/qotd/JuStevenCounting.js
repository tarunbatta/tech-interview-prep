/*
Ju Steven’s Counting
---------------------
Ju Steven, is an eccentric individual. He doesnt like odd numbers. The way he counts is 0,2,4,6,8,20,22,24,26 .. and so on.
Write a function that convert from normal number system to Ju Steven’s system and vice versa.

normal 0 = Ju Steven’s 0
normal 1 = Ju Steven’s 2
normal 2 = Ju Steven’s 4

Note: Try Ju Steven Counting Except 6.
*/
(function() {
    function GetJuStevenCountingHash() {
        var hash = [];
        hash.push({
            key: 1,
            value: 2
        });
        hash.push({
            key: 2,
            value: 4
        });
        hash.push({
            key: 3,
            value: 6
        });
        hash.push({
            key: 4,
            value: 8
        });

        return hash;
    }

    function GetJuStevenCountingExcept6Hash() {
        var hash = [];
        hash.push({
            key: 1,
            value: 2
        });
        hash.push({
            key: 2,
            value: 4
        });
        hash.push({
            key: 3,
            value: 8
        });

        return hash;
    }

    /*
    Time Complexity: O(log(n))
    Space Complexity: 0(1)
    */
    var convertDecimalToJuStevenCounting = function(num) {
        var result = 0;

        if (num != null && num > 0) {
            var hash = GetJuStevenCountingHash();

            var base = 5;
            var quotient = num;
            var remainder = 0;
            var multiplier = 1;

            while (quotient > 0) {
                remainder = quotient % base;
                quotient = parseInt(quotient / base);
                var mappedHashItem = hash.find(x => x.key == remainder);
                result += (mappedHashItem == null ? remainder : mappedHashItem.value) * multiplier;
                multiplier *= 10;
            }
        }

        return result;
    };

    /*
    Time Complexity: O(log(n))
    Space Complexity: 0(1)
    */
    var convertJuStevenCountingToDecimal = function(num) {
        var result = 0;

        if (num != null && num > 0) {
            var hash = GetJuStevenCountingHash();

            var decimalBase = 10;
            var juStevenBase = 5;
            var quotient = num;
            var remainder = 0;
            var exponent = 0;

            while (quotient > 0) {
                remainder = quotient % decimalBase;
                quotient = parseInt(quotient / decimalBase);

                var mappedHashItem = hash.find(x => x.value == remainder);
                result += Math.pow(juStevenBase, exponent) * (mappedHashItem == null ? remainder : mappedHashItem.key);
                exponent++;
            }
        }

        return result;
    };

    /*
    Time Complexity: O(log(n))
    Space Complexity: 0(1)
    */
    var convertDecimalToJuStevenCountingExcept6 = function(num) {
        var result = 0;

        if (num != null && num > 0) {
            var hash = GetJuStevenCountingExcept6Hash();

            var base = 4;
            var quotient = num;
            var remainder = 0;
            var multiplier = 1;

            while (quotient > 0) {
                remainder = quotient % base;
                quotient = parseInt(quotient / base);
                var mappedHashItem = hash.find(x => x.key == remainder);
                result += (mappedHashItem == null ? remainder : mappedHashItem.value) * multiplier;
                multiplier *= 10;
            }
        }

        return result;
    };

    /*
    Time Complexity: O(log(n))
    Space Complexity: 0(1)
    */
    var convertJuStevenCountingExcept6ToDecimal = function(num) {
        var result = 0;

        if (num != null && num > 0) {
            var hash = GetJuStevenCountingExcept6Hash();

            var decimalBase = 10;
            var juStevenBase = 4;
            var quotient = num;
            var remainder = 0;
            var exponent = 0;

            while (quotient > 0) {
                remainder = quotient % decimalBase;
                quotient = parseInt(quotient / decimalBase);

                var mappedHashItem = hash.find(x => x.value == remainder);
                result += Math.pow(juStevenBase, exponent) * (mappedHashItem == null ? remainder : mappedHashItem.key);
                exponent++;
            }
        }

        return result;
    };

    console.log("null:", convertDecimalToJuStevenCounting(null));
    console.log("undefined:", convertDecimalToJuStevenCounting(undefined));
    console.log("0:", convertDecimalToJuStevenCounting(0));
    console.log("1:", convertDecimalToJuStevenCounting(1));
    console.log("2:", convertDecimalToJuStevenCounting(2));
    console.log("3:", convertDecimalToJuStevenCounting(3));
    console.log("4:", convertDecimalToJuStevenCounting(4));
    console.log("5:", convertDecimalToJuStevenCounting(5));
    console.log("15:", convertDecimalToJuStevenCounting(15));
    console.log("99:", convertDecimalToJuStevenCounting(99));
    console.log("120:", convertDecimalToJuStevenCounting(120));

    console.log("null:", convertJuStevenCountingToDecimal(null));
    console.log("undefined:", convertJuStevenCountingToDecimal(undefined));
    console.log("0:", convertJuStevenCountingToDecimal(0));
    console.log("2:", convertJuStevenCountingToDecimal(2));
    console.log("4:", convertJuStevenCountingToDecimal(4));
    console.log("6:", convertJuStevenCountingToDecimal(6));
    console.log("8:", convertJuStevenCountingToDecimal(8));
    console.log("20:", convertJuStevenCountingToDecimal(20));
    console.log("60:", convertJuStevenCountingToDecimal(60));
    console.log("688:", convertJuStevenCountingToDecimal(688));
    console.log("880:", convertJuStevenCountingToDecimal(880));

    console.log("null:", convertDecimalToJuStevenCountingExcept6(null));
    console.log("undefined:", convertDecimalToJuStevenCountingExcept6(undefined));
    console.log("0:", convertDecimalToJuStevenCountingExcept6(0));
    console.log("1:", convertDecimalToJuStevenCountingExcept6(1));
    console.log("2:", convertDecimalToJuStevenCountingExcept6(2));
    console.log("3:", convertDecimalToJuStevenCountingExcept6(3));
    console.log("4:", convertDecimalToJuStevenCountingExcept6(4));
    console.log("5:", convertDecimalToJuStevenCountingExcept6(5));
    console.log("15:", convertDecimalToJuStevenCountingExcept6(15));
    console.log("99:", convertDecimalToJuStevenCountingExcept6(99));
    console.log("120:", convertDecimalToJuStevenCountingExcept6(120));

    console.log("null:", convertJuStevenCountingExcept6ToDecimal(null));
    console.log("undefined:", convertJuStevenCountingExcept6ToDecimal(undefined));
    console.log("0:", convertJuStevenCountingExcept6ToDecimal(0));
    console.log("2:", convertJuStevenCountingExcept6ToDecimal(2));
    console.log("4:", convertJuStevenCountingExcept6ToDecimal(4));
    console.log("8:", convertJuStevenCountingExcept6ToDecimal(8));
    console.log("20:", convertJuStevenCountingExcept6ToDecimal(20));
    console.log("22:", convertJuStevenCountingExcept6ToDecimal(22));
    console.log("88:", convertJuStevenCountingExcept6ToDecimal(88));
    console.log("2408:", convertJuStevenCountingExcept6ToDecimal(2408));
    console.log("2840:", convertJuStevenCountingExcept6ToDecimal(2840));

})();