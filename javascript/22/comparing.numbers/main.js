var numbers = require('./numbers.js').numbers;
var readline = require('readline');

var rl = readline.createInterface(process.stdin, process.stdout);

function prompt() {
  process.stdout.write("Enter a number: ");
}

rl.on('line', function(line) {
  if (line.match(/^[0-9]*$/)) {
    numbers.add(line);
  }
  if (numbers.complete()) {
    rl.close();
  }
  if (numbers.same()) {
    console.log('Numbers must be unique.')
    process.exit(0);
  }
  prompt();
}).on('close', function() {
  process.stdout.write("The largest number is " + numbers.max() + ".\n");
});

prompt();