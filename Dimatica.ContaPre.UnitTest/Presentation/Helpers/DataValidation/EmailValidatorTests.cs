using Dimatica.ContaPre.Presentation.DataValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.Presentation.Helpers.DataValidation
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

        [TestMethod()]
        public void IsValidCorporateEmail_SubdomainOfUimpIsAccepted()
        {
            Assert.IsTrue(EmailValidator.IsValidCorporateEmail("persona@sub.uimp.es"));
        }

        [TestMethod()]
        public void IsValidCorporateEmail_NonCorporateDomainIsRejected()
        {
            Assert.IsFalse(EmailValidator.IsValidCorporateEmail("persona@uimp.com"));
        }

        [TestMethod()]
        public void IsValidCorporateEmail_MalformedAddressIsRejected()
        {
            Assert.IsFalse(EmailValidator.IsValidCorporateEmail("persona-uimp.es"));
        }
    }
}
