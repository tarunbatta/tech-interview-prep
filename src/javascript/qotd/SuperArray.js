/*
Building a Super Array
---------------------------------------
Can you build a stack like data structure that stores an integer and supports the following operations.
a) Push
b) Pop
c) Peek
d) Find Average of all elements in the array
e) Find Maximum value in the Stack
f) Find the Median of the value in the Stack
*/
(function() {
    function SuperArray() {
        this.data = [];
        this.top = -1;
        this.sum = 0;
        this.max = [];
        this.median = [];
    }

    SuperArray.prototype._displayStack = function(value) {
        console.log('-----------------------------------------');
        console.log('Stack: ' + this.data.join(','));
        console.log('Peek: ' + this.peek() + '.');
        console.log('Average: ' + this.stackAverage() + '.');
        console.log('Max: ' + this.stackMax() + '.');
        console.log('Median: ' + this.stackMedian() + '.');
        console.log('-----------------------------------------');
    };

    SuperArray.prototype.push = function(value) {
        if (value !== null && typeof(value) === 'number') {
            this.top++;

            this.data[this.top] = value;

            this.median[this.top] = value;
            this.median.sort();

            this.sum += value;

            if (this.top === 0) {
                this.max[this.top] = value;
            } else {
                if (value > this.max[this.top - 1]) {
                    this.max[this.top] = value;
                } else {
                    this.max[this.top] = this.max[this.top - 1];
                }
            }

            console.log('PUSH: ' + value + ' successfully pushed to stack.');
            this._displayStack();
        } else {
            console.log('PUSH: Please enter a valid integer.');
        }
    };

    SuperArray.prototype.pop = function() {
        var result = -1;

        if (this.top > -1 && this.data.length > 0) {
            result = this.data[this.top];

            this.data.splice(this.top, 1);
            this.max.splice(this.top, 1);

            this.median.splice(this.median.indexOf(result), 1);
            this.median.sort();

            this.sum -= result;

            this.top--;

            console.log('POP: ' + result + ' successfully poped from stack.');
            this._displayStack();
        } else {
            console.log('POP: Nothing to pop.');
        }

        return result;
    };

    SuperArray.prototype.peek = function() {
        var result = Number.MIN_SAFE_INTEGER;

        if (this.top > -1 && this.data.length > 0) {
            result = this.data[this.top];
        }

        return result;
    };

    SuperArray.prototype.stackAverage = function() {
        var avg = 0;

        if (this.data !== null && this.data.length > 0) {
            avg = this.sum / this.data.length;
        }

        return avg;
    };

    SuperArray.prototype.stackMax = function() {
        var result = Number.MIN_SAFE_INTEGER;

        if (this.top > -1 && this.max.length > 0) {
            result = this.max[this.top];
        }

        return result;
    };

    SuperArray.prototype.stackMedian = function() {
        var result = Number.MIN_SAFE_INTEGER;

        if (this.top > -1 && this.data.length > 0) {
            if (this.data.length == 1) {
                result = this.median[0];
            } else {
                var mid = parseInt(this.data.length / 2);
                if (this.data.length % 2 === 0) {
                    result = (this.median[mid] + this.median[mid - 1]) / 2;
                } else {
                    result = this.median[mid];
                }
            }
        }

        return result;
    };

    var sa = new SuperArray();
    sa.push('');
    sa.push(null);
    sa.push(undefined);
    sa.push(1);
    sa.push(2);
    sa.push(3);
    sa.push(4);
    sa.push(5);
    sa.pop();
    sa.pop();
    sa.pop();
    sa.pop();
    sa.pop();
    sa.pop();
    sa.push(5);
    sa.push(4);
    sa.push(3);
    sa.pop();
    sa.pop();
    sa.pop();
})();