using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Addition01Test()
        {
            int a = 2;
            int b = 3;
            int result = a + b;

            Assert.AreEqual(5, result, "La suma de 2 y 3 debería ser 5.");
        }

        [TestMethod]
        public void Addition02Test()
        {
            int a = 2;
            int b = 3;
            int result = a + b;

            Assert.AreNotEqual(6, result, "La suma de 2 y 3 no debería ser 6.");
        }


        [TestMethod]
        public void Addition03Test()
        {
            int a = 3;
            int b = 3;
            bool result = a == b;

            Assert.IsTrue(result, "La suma de 2 y 3 no debería ser 6.");
        }
    }
}
