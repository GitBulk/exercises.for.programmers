using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._15
{
    public class ConsolePasswordValidation
    {
        public void Execute()
        {
            var username = Input.ParseString(message: "What is the username: ");
            var password = Input.ParseString(message: "What is the password: ");

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

    class PasswordValidator
    {
        public static bool Validate(string username, string password)
        {
            if (UserNameValid(username))
            {
                using (var crypto = SHA256.Create())
                {
                    var hashedPassword = crypto.ComputeHash(Encoding.Unicode.GetBytes(password));
                    return Password(username).Equals(Convert.ToBase64String(hashedPassword));
                }
            }
            else
            {
                return false;
            }
        }

        private static string Password(string username)
        {
            return Credentials()[username];
        }

        private static bool UserNameValid(string username)
        {
            return Credentials().ContainsKey(username);
        }

        private static Dictionary<string, string> Credentials()
        {
            // Would add a salt additionally when storing for real. Each set of credentials
            // would be assigned a unique salt when saved.
            return new Dictionary<string, string>()
                   {
                       {"validUser", "4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="},
                       {"invalidUser", "nUuyFJB59QnJX3vF3Ssegp8jlV7VZPuYmmaGHPz5Cjo="}
                   };
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
}