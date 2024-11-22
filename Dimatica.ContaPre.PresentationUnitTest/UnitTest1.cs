using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AdditionTest()
        {
            int a = 2;
            int b = 3;
            int result = a + b;

            Assert.AreEqual(5, result, "La suma de 2 y 3 debería ser 5.");
        }
    }
}
