using Dimatica.ContaPre.Presentation.Helpers;
using Dimatica.ContaPre.Presentation.Helpers.Session;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers
{
    [TestClass]
    public class SessionHelperTests
    {
        [TestMethod]
        public void EncryptAndDecrypt_RoundTripReturnsOriginalValue()
        {
            const string clearText = "session-value-123";

            var cipherText = SessionHelper.Encrypt(clearText);

            Assert.IsTrue(SessionHelper.IsValidEncryption(cipherText));
            Assert.AreEqual(clearText, SessionHelper.Decrypt(cipherText));
        }

        [TestMethod]
        public void IsValidEncryption_InvalidValueReturnsFalse()
        {
            Assert.IsFalse(SessionHelper.IsValidEncryption("not-a-valid-cipher"));
        }

        [TestMethod]
        public void Encrypt_EmptyValueReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, SessionHelper.Encrypt(string.Empty));
        }
    }
}
