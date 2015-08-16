using System;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise5
{
    public class ConsoleSimpleMath : IUserAdapter
    {
        public void Execute()
        {
            var simpleMath = new SimpleMath();

            Console.WriteLine("What is the first number?");
            simpleMath.AskForFirstNumber(this);

            Console.WriteLine("What is the second number?");
            simpleMath.AskForSecondNumber(this);

            var results = simpleMath.Results();

            Console.WriteLine("{0}\n{1}\n{2}\n{3}", 
                results.Addition,
                results.Subtraction,
                results.Multiplication,
                results.Division);
        }

        public string UserInput()
        {
            return Console.ReadLine();
        }

        public void UserInputFailure()
        {
            Console.WriteLine("You must enter a valid, positive number.");
        }
    }

    public class SimpleMath
    {
        private int firstNumber;
        private int secondNumber;

        public int AskForFirstNumber(IUserAdapter userAdapter)
        {
            while (true)
            {
                try
                {
                    this.firstNumber = int.Parse(userAdapter.UserInput());
                    if (this.firstNumber < 0)
                    {
                        userAdapter.UserInputFailure();
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    userAdapter.UserInputFailure();
                }
            }

            return this.firstNumber;
        }

        public int AskForSecondNumber(IUserAdapter userAdapter)
        {
            while (true)
            {
                try
                {
                    this.secondNumber = int.Parse(userAdapter.UserInput());
                    if (this.secondNumber < 0)
                    {
                        userAdapter.UserInputFailure();
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    userAdapter.UserInputFailure();
                }
            }

            return this.secondNumber;
        }

        public Result Results()
        {
            return new Result()
                   {
                       Addition = AddNumbers(),
                       Subtraction = SubtractNumbers(),
                       Multiplication = MultipleNumbers(),
                       Division = DivideNumbers()
                   };
        }

        private string DivideNumbers()
        {
            var total = this.firstNumber / this.secondNumber;
            return string.Format("{0} / {1} = {2}", this.firstNumber, this.secondNumber, total);
        }

        private string SubtractNumbers()
        {
            var total = this.firstNumber - this.secondNumber;
            return string.Format("{0} - {1} = {2}", this.firstNumber, this.secondNumber, total);
        }

        private string AddNumbers()
        {
            var total = this.firstNumber + this.secondNumber;
            return string.Format("{0} + {1} = {2}", this.firstNumber, this.secondNumber, total);
        }

        private string MultipleNumbers()
        {
            var total = this.firstNumber * this.secondNumber;
            return string.Format("{0} * {1} = {2}", this.firstNumber, this.secondNumber, total);
        }
    }

    public interface IUserAdapter
    {
        string UserInput();

        void UserInputFailure();
    }

    public class Result
    {
        public string Addition { get; set; }

        public string Subtraction { get; set; }
        
        public string Multiplication { get; set; }

        public string Division { get; set; }
    }

    [TestFixture]
    public class SimpleMathTests
    {
        [TestFixture]
        public class First_Number_Tests
        {
            [Test]
            public void Asking_For_First_Number()
            {
                int result = new SimpleMath().AskForFirstNumber(new StubUserAdapter("147"));
                Assert.That(result, Is.EqualTo(147));
            }

            [Test]
            public void Asking_For_Invalid_Number_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("147a", "147");
                int result = new SimpleMath().AskForFirstNumber(userAdapter);
                Assert.That(result, Is.EqualTo(147));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(1));
            }

            [Test]
            public void Asking_For_Negative_Number_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("-147", "147");
                int result = new SimpleMath().AskForFirstNumber(userAdapter);
                Assert.That(result, Is.EqualTo(147));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(1));
            }

            [Test]
            public void Asking_For_Invalid_Number_Multiple_Times_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("147a", "4a", "b", "147");
                int result = new SimpleMath().AskForFirstNumber(userAdapter);
                Assert.That(result, Is.EqualTo(147));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(3));
            }

            [Test]
            public void Asking_For_Negative_Number_Multiple_Times_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("-147", "-4", "147");
                int result = new SimpleMath().AskForFirstNumber(userAdapter);
                Assert.That(result, Is.EqualTo(147));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class Second_Number_Tests
        {
            [Test]
            public void Asking_For_Second_Number()
            {
                int result = new SimpleMath().AskForSecondNumber(new StubUserAdapter("94"));
                Assert.That(result, Is.EqualTo(94));
            }

            [Test]
            public void Asking_For_Invalid_Number_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("94a", "94");
                int result = new SimpleMath().AskForSecondNumber(userAdapter);
                Assert.That(result, Is.EqualTo(94));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(1));
            }

            [Test]
            public void Asking_For_Negative_Number_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("-94", "94");
                int result = new SimpleMath().AskForSecondNumber(userAdapter);
                Assert.That(result, Is.EqualTo(94));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(1));
            }

            [Test]
            public void Asking_For_Invalid_Number_Multiple_Times_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("94a", "4a", "b", "94");
                int result = new SimpleMath().AskForSecondNumber(userAdapter);
                Assert.That(result, Is.EqualTo(94));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(3));
            }

            [Test]
            public void Asking_For_Negative_Number_Multiple_Times_Prompts_For_Retry()
            {
                var userAdapter = new StubUserAdapter("-94a", "-4", "94");
                int result = new SimpleMath().AskForSecondNumber(userAdapter);
                Assert.That(result, Is.EqualTo(94));
                Assert.That(userAdapter.UserInputFailureCount, Is.EqualTo(2));
            }
        }
        
        [Test]
        public void Formatting_Results_For_Addition()
        {
            var simpleMath = new SimpleMath();
            simpleMath.AskForFirstNumber(new StubUserAdapter("10"));
            simpleMath.AskForSecondNumber(new StubUserAdapter("5"));

            Result formattedResults = simpleMath.Results();

            Assert.That(formattedResults.Addition, Is.EqualTo("10 + 5 = 15"));
        }

        [Test]
        public void Formatting_Results_For_Subtraction()
        {
            var simpleMath = new SimpleMath();
            simpleMath.AskForFirstNumber(new StubUserAdapter("10"));
            simpleMath.AskForSecondNumber(new StubUserAdapter("5"));

            Result formattedResults = simpleMath.Results();

            Assert.That(formattedResults.Subtraction, Is.EqualTo("10 - 5 = 5"));
        }

        [Test]
        public void Formatting_Results_For_Multiplication()
        {
            var simpleMath = new SimpleMath();
            simpleMath.AskForFirstNumber(new StubUserAdapter("10"));
            simpleMath.AskForSecondNumber(new StubUserAdapter("5"));

            Result formattedResults = simpleMath.Results();

            Assert.That(formattedResults.Multiplication, Is.EqualTo("10 * 5 = 50"));
        }

        [Test]
        public void Formatting_Results_For_Division()
        {
            var simpleMath = new SimpleMath();
            simpleMath.AskForFirstNumber(new StubUserAdapter("10"));
            simpleMath.AskForSecondNumber(new StubUserAdapter("5"));

            Result formattedResults = simpleMath.Results();

            Assert.That(formattedResults.Division, Is.EqualTo("10 / 5 = 2"));
        }

        public class StubUserAdapter : IUserAdapter
        {
            private readonly string[] input;
            private int index;

            public StubUserAdapter(params string[] input)
            {
                this.index = 0;
                this.input = input;
                UserInputFailureCount = 0;
            }

            public int UserInputFailureCount { get; private set; }

            public string UserInput()
            {
                var first = input[index];
                this.index++;
                return first;
            }

            public void UserInputFailure()
            {
                UserInputFailureCount++;
            }
        }
    }
}