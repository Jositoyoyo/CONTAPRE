using Dimatica.ContaPre.Presentation.Helpers.DataValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers.DataValidation
{
    [TestClass]
    public class IbanValidatorTest
    {
        [TestMethod]
        public void ValidateIban_ValidData_ReturnsTrue()
        {
            // Datos de entrada válidos
            string ccEntity = "2080";
            string ccBranch = "6489";
            string ccDc = "15";
            string ccAccount = "6470732359";
            string expectedIban = "ES7720806489156470732359"; 

            // Llamada al método a probar
            bool result = IbanValidator.ValidateIban(ccEntity, ccBranch, ccDc, ccAccount, out string realIban);

            // Verificaciones
            Assert.IsTrue(result, "Se esperaba que el IBAN fuera válido.");
            Assert.AreEqual(expectedIban, realIban, "El IBAN generado no coincide con el esperado.");
        }

        [TestMethod]
        public void ValidateIban_EmptyFields_ReturnsTrue()
        {
            // Todos los campos vacíos
            string ccEntity = "";
            string ccBranch = "";
            string ccDc = "";
            string ccAccount = "";

            // Llamada al método a probar
            bool result = IbanValidator.ValidateIban(ccEntity, ccBranch, ccDc, ccAccount, out string realIban);

            // Verificaciones
            Assert.IsFalse(result, "Se esperaba que el resultado fuera verdadero para campos vacíos.");
            Assert.AreEqual(string.Empty, realIban, "Se esperaba que el IBAN generado estuviera vacío.");
        }

        [TestMethod]
        public void ValidateIban_InvalidLength_ReturnsFalse()
        {
            // Longitud incorrecta
            string ccEntity = "123";
            string ccBranch = "5678";
            string ccDc = "90";
            string ccAccount = "1234567890";

            // Llamada al método a probar
            bool result = IbanValidator.ValidateIban(ccEntity, ccBranch, ccDc, ccAccount, out string realIban);

            // Verificaciones
            Assert.IsFalse(result, "Se esperaba que el IBAN fuera inválido debido a longitud incorrecta.");
            Assert.AreEqual(string.Empty, realIban, "Se esperaba que el IBAN generado estuviera vacío.");
        }

        [TestMethod]
        public void ValidateIban_InvalidMod97_ReturnsFalse()
        {
            // Datos que producirán un IBAN con módulo 97 inválido
            string ccEntity = "A000";
            string ccBranch = "0000";
            string ccDc = "00";
            string ccAccount = "0000000000";

            // Llamada al método a probar
            bool result = IbanValidator.ValidateIban(ccEntity, ccBranch, ccDc, ccAccount, out string realIban);

            // Verificaciones
            Assert.IsFalse(result, "Se esperaba que el IBAN fuera inválido debido a un módulo 97 incorrecto.");
            Assert.AreEqual(string.Empty, realIban, "Se esperaba que el IBAN generado estuviera vacío.");
        }

        [TestMethod]
        public void ValidateIban_NonNumericField_ReturnsFalse()
        {
            bool result = IbanValidator.ValidateIban(
                "20A0",
                "6489",
                "15",
                "6470732359",
                out string realIban);

            Assert.IsFalse(result);
            Assert.AreEqual(string.Empty, realIban);
        }

        [TestMethod]
        public void ValidateIban_PartiallyFilledFields_ReturnsFalse()
        {
            bool result = IbanValidator.ValidateIban(
                "2080",
                "6489",
                "15",
                "",
                out string realIban);

            Assert.IsFalse(result);
            Assert.AreEqual(string.Empty, realIban);
        }
    }
}
