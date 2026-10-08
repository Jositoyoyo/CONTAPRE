using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimatica.ContaPre.Presentation.Helpers;
using Dimatica.ContaPre.Presentation.Helpers.PlainPassword;
using System.Linq;

namespace Dimatica.ContaPre.UnitTest.Presentation.Helpers.DataValidation
{
    [TestClass()]
    public class PlainPasswordGeneratorTests
    {
        [TestMethod()]
        public void GeneratePassword01Test()
        {
            string plainPasswordGenerator = PlainPasswordGenerator.GeneratePassword(10);
            Assert.AreEqual(10, plainPasswordGenerator.Length);
        }

        [TestMethod()]
        public void GeneratePassword02Test()
        {
            string plainPasswordGenerator = PlainPasswordGenerator.GeneratePassword(10);
            Assert.IsTrue(plainPasswordGenerator.All(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Contains(c)));
        }

        [TestMethod()]
        public void GeneratePassword_LengthBelowMinimumIsClampedToEight()
        {
            Assert.AreEqual(8, PlainPasswordGenerator.GeneratePassword(1).Length);
        }

        [TestMethod()]
        public void GeneratePassword_ContainsUppercaseAndDigit()
        {
            string password = PlainPasswordGenerator.GeneratePassword(32);

            Assert.IsTrue(password.Any(char.IsUpper));
            Assert.IsTrue(password.Any(char.IsDigit));
        }

        [TestMethod()]
        public void SimpleGeneratePassword_UsesRequestedLength()
        {
            Assert.AreEqual(12, PlainPasswordGenerator.simpleGeneratePassword(12).Length);
        }
    }
}
