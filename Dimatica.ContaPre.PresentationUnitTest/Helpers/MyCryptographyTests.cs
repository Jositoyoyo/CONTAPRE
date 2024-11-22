using Dimatica.ContaPre.Presentation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers
{

    [TestClass()]
    public class MyCryptographyTests
    {

        [TestMethod()]
        public void IsNotValidEncryptionTest()
        {
            string cipherText = "ksjklfjlksjlkfjlkj";
            bool IsValidEncryption = MyCryptography.IsValidEncryption(cipherText);
            Assert.IsFalse(IsValidEncryption);
        }

        [TestMethod()]
        public void IsValidEncryptionTest()
        {
            string cipherText = "yEolebMPZzxv+WPamxztkyrVYU2Qe3Jif4a9L2FSF4Y=";
            bool IsValidEncryption = MyCryptography.IsValidEncryption(cipherText);
            Assert.IsTrue(IsValidEncryption);
        }

        [TestMethod()]
        public void EncryptDecryptConsistencyTest()
        {
            // Texto en claro para probar el ciclo completo de encriptación/desencriptación
            string clearText = "TextoDePrueba123";

            // Encriptar
            string cipherText = MyCryptography.Encrypt(clearText);
            Assert.IsNotNull(cipherText);
            Assert.AreNotEqual(clearText, cipherText); // Deben ser diferentes

            // Desencriptar y verificar si se obtiene el mismo valor original
            string decryptedText = MyCryptography.Decrypt(cipherText);
            Assert.AreEqual(clearText, decryptedText); // Debe ser igual al texto original
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // Texto en claro para encriptar
            string clearText = "TextoDePruebaParaEncriptar";

            // Llamar al método Encrypt
            string cipherText = MyCryptography.Encrypt(clearText);

            // Comprobar que el texto cifrado no es nulo ni vacío
            Assert.IsNotNull(cipherText);
            Assert.IsFalse(string.IsNullOrEmpty(cipherText));

            // Comprobar que el texto cifrado está en formato Base64 (validación de formato)
            try
            {
                Convert.FromBase64String(cipherText); // Si falla, no es un Base64 válido
            }
            catch (FormatException)
            {
                Assert.Fail("El texto cifrado no está en formato Base64.");
            }

            // Asegurarse de que el texto cifrado no es igual al texto en claro
            Assert.AreNotEqual(clearText, cipherText);
        }

    }
}
