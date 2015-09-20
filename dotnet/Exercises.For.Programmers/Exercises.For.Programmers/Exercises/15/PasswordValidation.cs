using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._15
{
    public class ConsolePasswordValidation
    {
        public void Execute()
        {
            var username = Input.ParseString(message: "What is the username: ");
            var password = Input.ParsePassword(message: "What is the password: ");

            if (PasswordValidator.Validate(username, password))
            {
                Console.WriteLine("Welcome!");
            }
            else
            {
                Console.WriteLine("That password is incorrect.");
            } 
        }
    }

    [TestFixture]
    class Tests
    {
        class Valid_Attempts
        {
            [Test]
            public void Valid_Password_And_Username()
            {
                bool attemptedPassword = PasswordValidator.Validate("validUser", "password");
                Assert.That(attemptedPassword, Is.True);
            }
        }

        class Invalid_Attempts
        {
            [Test]
            public void Invalid_Password()
            {
                bool attemptedPassword = PasswordValidator.Validate("invalidUser", "password");
                Assert.That(attemptedPassword, Is.False);
            }

            [Test]
            public void Invalid_Username()
            {
                bool attemptedPassword = PasswordValidator.Validate("invalidUsername", "password1");
                Assert.That(attemptedPassword, Is.False);
            }

            [Test]
            public void Invalid_Password_Attempt_With_Varying_Casing()
            {
                bool attemptedPassword = PasswordValidator.Validate("validUser", "PaSSword");
                Assert.That(attemptedPassword, Is.False);
            }
        }
    }

    class PasswordValidator
    {
        public static bool Validate(string username, string password)
        {
            string actualPassword;
            if (Credentials().TryGetValue(username, out actualPassword))
            {
                return actualPassword.Equals(password);
            }
            else
            {
                return false;
            }
        }

        private static Dictionary<string, string> Credentials()
        {
            return new Dictionary<string, string>()
                   {
                       {"validUser", "password"},
                       {"invalidUser", "password1"}
                   };
        }
    }
}