using Dimatica.ContaPre.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.Presentation.Helpers.DataValidation
{
    [TestClass]
    public class IdentificationValidatorTest
    {
        private IdentificationValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new IdentificationValidator();
        }

        [TestMethod]
        public void ValidateNif_ValidNif_ReturnsTrue()
        {

            var validNif = "12345678Z"; // Ejemplo de NIF válido
            var result = _validator.ValidateDocument(validNif);

            Assert.IsTrue(result, $"El NIF '{validNif}' debería ser válido.");
        }

        [TestMethod]
        public void ValidateNif_InvalidNif_ReturnsFalse()
        {

            var invalidNif = "12345678A"; // Ejemplo de NIF inválido
            var result = _validator.ValidateDocument(invalidNif);

            Assert.IsFalse(result, $"El NIF '{invalidNif}' debería ser inválido.");
        }

        [TestMethod]
        public void ValidateNif_EmptyNif_ReturnsFalse()
        {
            var emptyNif = "";
            var result = _validator.ValidateDocument(emptyNif);

            Assert.IsFalse(result, "Un NIF vacío debería ser inválido.");
        }

        [TestMethod]
        public void ValidateNif_ShortNif_ReturnsFalse()
        {
            var shortNif = "1234Z"; // NIF con longitud insuficiente
            var result = _validator.ValidateDocument(shortNif);

            Assert.IsFalse(result, $"El NIF '{shortNif}' debería ser inválido.");
        }

        [TestMethod]
        public void ValidateCif_ValidCif_ReturnsTrue()
        {

            var validCif = "S2693959E"; // Ejemplo de CIF válido
            var result = _validator.ValidateCIF(validCif);

            Assert.IsTrue(result, $"El CIF '{validCif}' debería ser válido.");
        }

        [TestMethod]
        public void ValidateCif_InvalidCif_ReturnsFalse()
        {
            var invalidCif = "A1234"; // CIF inválido
            var result = _validator.ValidateCIF(invalidCif);

            Assert.IsFalse(result, $"El CIF '{invalidCif}' debería ser inválido.");
        }

        [TestMethod]
        public void ValidateNie_ValidNie_ReturnsTrue()
        {

            var validNie = "Y7790858J"; // Ejemplo de NIE válido
            var result = _validator.ValidNIE(validNie);

            Assert.IsTrue(result, $"El NIE '{validNie}' debería ser válido.");
        }

        [TestMethod]
        public void ValidateNie_InvalidNie_ReturnsFalse()
        {

            var invalidNie = "X1234567A"; // Ejemplo de NIE inválido
            var result = _validator.ValidNIE(invalidNie);
            Assert.IsFalse(result, $"El NIE '{invalidNie}' debería ser inválido.");
        }

        [TestMethod]
        public void ValidateDocument_ValidCif_ReturnsTrue()
        {
            Assert.IsTrue(_validator.ValidateDocument("S2693959E"));
        }

        [TestMethod]
        public void ValidateDocument_ValidNie_ReturnsTrue()
        {
            Assert.IsTrue(_validator.ValidateDocument("Y7790858J"));
        }

        [TestMethod]
        public void ValidateDocument_UnsupportedFormat_ReturnsFalse()
        {
            Assert.IsFalse(_validator.ValidateDocument("12345678"));
        }
    
    }
}
