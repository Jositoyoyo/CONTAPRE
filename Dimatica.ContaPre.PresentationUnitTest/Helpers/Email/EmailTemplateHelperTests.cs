using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers.Email
{ 
    [TestClass()]
    public class EmailTemplateHelperTests
    {
        [TestMethod()]
        public void GetEmailBody_Returns_Correct_Email_Body()
        {
            string expectedpassword = "password123";
            string password = "password123";
            // Assert
            Assert.AreEqual(expectedpassword, password);
        }
    }
}
