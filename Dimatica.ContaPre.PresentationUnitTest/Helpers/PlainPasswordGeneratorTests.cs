using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimatica.ContaPre.Presentation.Helpers;
using System.Linq;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers.DataValidation
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
    }
}