var numbers = {
  nums : [],
  add : function(number) {
    this.nums.push(number);
  },
  complete : function() {
    return this.nums.length === 3;
  },
  max : function() {
    var max = this.nums[0];
    for (var i = 0; i < this.nums.length; i++) {
      if (this.nums[i] > max) {
        max = this.nums[i];
      }
    }
    return max;
  },
  same : function() {
    return false;
  }
};

module.exports.numbers = numbers;