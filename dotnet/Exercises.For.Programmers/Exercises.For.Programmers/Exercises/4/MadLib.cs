using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._4
{
    public class ConsoleMadLib
    {
        public void Execute()
        {
            var madLib = new MadLib();

            Console.Write("Enter a noun: ");
            madLib.Noun(Console.ReadLine());

            Console.Write("Enter a verb: ");
            madLib.Verb(Console.ReadLine());

            Console.Write("Enter an adjective: ");
            madLib.Adjective(Console.ReadLine());

            Console.Write("Enter an adverb: ");
            madLib.Adverb(Console.ReadLine());

            Console.WriteLine(madLib.Story());
        }
    }

    public class MadLib
    {
        private string noun;
        private string verb;
        private string adverb;
        private string adjective;

        public MadLib Noun(string noun)
        {
            this.noun = noun;
            return this;
        }

        public MadLib Verb(string verb)
        {
            this.verb = verb;
            return this;
        }

        public MadLib Adverb(string adverb)
        {
            this.adverb = adverb;
            return this;
        }

        public MadLib Adjective(string adjective)
        {
            this.adjective = adjective;
            return this;
        }

        public string Story()
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(verb), "Verb null or whitespace.");
            Debug.Assert(!string.IsNullOrWhiteSpace(adverb), "Adverb null or whitespace.");
            Debug.Assert(!string.IsNullOrWhiteSpace(noun), "Noun null or whitespace.");
            Debug.Assert(!string.IsNullOrWhiteSpace(adjective), "Adjective null or whitespace.");

            return string.Format(
                "Do you {0} your {1} {2} {3}? That's hilarious!",
                this.verb,
                this.adjective,
                this.noun,
                this.adverb);
        }
    }
   
    [TestFixture]
    public class MadLibTests
    {
        [Test]
        public void Telling_A_Story()
        {
            string story = new MadLib()
                .Noun("dog")
                .Verb("walk")
                .Adverb("quickly")
                .Adjective("blue")
                .Story();

            Assert.That(story, Is.EqualTo(
                "Do you walk your blue dog quickly? That's hilarious!"));
        }
    }
}