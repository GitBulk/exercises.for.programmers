var numbers = {
  nums : [],
  add : function(number) {
    this.nums.push(number);
  },
  complete : function() {
    var MAX_NUMBERS = 3;
    return this.nums.length === MAX_NUMBERS;
  },
  max : function() {
    var max = this.nums[0];
    for (var i = 0; i < this.nums.length; i++) {
      if (Number(this.nums[i]) > Number(max)) {
        max = this.nums[i];
      }
    }
    return max;
  },
  same : function() {
    for (var i = 0; i < this.nums.length; i++) {
      if (this.nums.indexOf(this.nums[i]) !== i) {
        return true;
      }
    }
    return false;
  }
};

module.exports.numbers = numbers;