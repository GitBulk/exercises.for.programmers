using System;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise2
{
    public class CharacterCount
    {
        internal string AskQuestion()
        {
            return "What is the input string?";
        }

        internal string AnswerQuestion(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "You must enter a string.";
            }

            return string.Format("{0} has {1} characters.", input, input.Length);
        }

        public void CountCharacters()
        {
            Console.Write("{0} ", AskQuestion());
            Console.WriteLine(AnswerQuestion(Console.ReadLine()));
        }
    }

    [TestFixture]
    public class Excercise2Tests
    {
        [Test]
        public void Ask_Question()
        {
            Assert.That(new CharacterCount().AskQuestion(), Is.EqualTo("What is the input string?"));
        }

        [Test]
        public void Answer_Length()
        {
            Assert.That(new CharacterCount().AnswerQuestion("String"), Is.EqualTo("String has 6 characters."));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void State_User_Must_Provide_Input_If_Not_Provided(string noInput)
        {
            Assert.That(new CharacterCount().AnswerQuestion(noInput), Is.EqualTo("You must enter a string."));
        }
    }
}