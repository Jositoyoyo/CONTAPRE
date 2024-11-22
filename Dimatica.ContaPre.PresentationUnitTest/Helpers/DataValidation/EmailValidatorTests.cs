using Dimatica.ContaPre.Presentation.DataValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers.DataValidation
{
    [TestClass()]
    public class EmailValidatorTests
    {
        [TestMethod()]
        public void IsValidCorporateEmailTest()
        {
            bool isValidCorporateEmailTest = EmailValidator.IsValidCorporateEmail("prue@yahoo.es");
            Assert.IsFalse(isValidCorporateEmailTest);
        }

        [TestMethod()]
        public void IsValidCorporateEmailTest2()
        {
            bool isValidCorporateEmailTest = EmailValidator.IsValidCorporateEmail("pruebas@uimp.es");
            Assert.IsTrue(isValidCorporateEmailTest);
        }
    }
}