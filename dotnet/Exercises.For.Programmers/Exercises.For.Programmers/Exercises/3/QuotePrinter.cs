using System;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._3
{
    public class ConsoleQuotePrinter
    {
        public void Execute()
        {
            var quotePrinter = new QuotePrinter();

            Console.Write("{0} ", quotePrinter.AskForQuote());
            var quote = Console.ReadLine();
            
            Console.Write("{0} ", quotePrinter.AskForAuthor());
            var author = Console.ReadLine();

            Console.WriteLine(quotePrinter.FormatQuote(author, quote));
        }
    }
   
    public class QuotePrinter
    {
        public string AskForQuote()
        {
            return "What is the quote?";
        }

        public string AskForAuthor()
        {
            return "Who said it?";
        }

        public string FormatQuote(string author, string quote)
        {
            const string quoteMarks = "\"";
            return author + " says, " + quoteMarks + quote + "." + quoteMarks;
        }
    }

    [TestFixture]
    public class QuotePrinterTests
    {
        [Test]
        public void Asking_For_A_Quote()
        {
            string quote = new QuotePrinter().AskForQuote();
            Assert.That(quote, Is.EqualTo("What is the quote?"));
        }

        [Test]
        public void Asking_For_An_Author()
        {
            string author = new QuotePrinter().AskForAuthor();
            Assert.That(author, Is.EqualTo("Who said it?"));
        }

        [Test]
        public void Formatting_Quote()
        {
            string formattedQuote = new QuotePrinter().FormatQuote("Shaun", "This is a quote");
            Assert.That(formattedQuote, Is.EqualTo("Shaun says, \"This is a quote.\""));
        }
    }
}