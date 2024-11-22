namespace Dimatica.ContaPre.Presentation.Helpers.DataValidation
{
    using System.Linq;
    using System.Numerics;

    public static class IbanValidator
    {
        public static bool ValidateIban(
            string ccEntity,
            string ccBranch,
            string ccDc,
            string ccAccount,
            out string realIban)
        {
            realIban = string.Empty;

            // Si todos los campos están vacíos, consideramos no válido
            if (string.IsNullOrWhiteSpace(ccEntity) &&
                string.IsNullOrWhiteSpace(ccBranch) &&
                string.IsNullOrWhiteSpace(ccDc) &&
                string.IsNullOrWhiteSpace(ccAccount))
            {
                return false;
            }

            // Validar que la longitud total sea exactamente 20 caracteres
            if ((ccEntity?.Length ?? 0) + (ccBranch?.Length ?? 0) + (ccDc?.Length ?? 0) + (ccAccount?.Length ?? 0) != 20)
            {
                return false;
            }

            // comrpobar que sean todos numericos menos realIban
            // Validar que todos los campos sean numéricos
            if (!IsNumeric(ccEntity) || !IsNumeric(ccBranch) || !IsNumeric(ccDc) || !IsNumeric(ccAccount))
            {
                return false;
            }

            // Construir la base del IBAN
            string iban = ccEntity + ccBranch;
            BigInteger mod1 = BigInteger.Parse(iban) % 97;

            iban = mod1 + ccDc + ccAccount.Substring(0, 2);
            mod1 = BigInteger.Parse(iban) % 97;

            iban = mod1 + ccAccount.Substring(2) + "142800";
            BigInteger modIban = BigInteger.Parse(iban) % 97;

            int ccIban = (int)(98 - modIban);
            string ccIbanStr = ccIban < 10 ? "0" + ccIban : ccIban.ToString();

            realIban = $"ES{ccIbanStr}{ccEntity}{ccBranch}{ccDc}{ccAccount}";
            realIban = realIban.ToUpper().Replace(" ", "");

            // Verificar longitud del IBAN
            if (realIban.Length != 24)
            {
                return false;
            }

            // Convertir letras iniciales a números
            int num1 = GetIbanLetterValue(realIban[0]);
            int num2 = GetIbanLetterValue(realIban[1]);

            // Reorganizar el IBAN
            string auxIban = realIban.Substring(4) + num1 + num2 + realIban.Substring(2, 2);

            // Validar el módulo 97
            return ValidateMod97(auxIban);
        }

        // Método auxiliar para validar si una cadena es numérica
        private static bool IsNumeric(string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
        }

        private static int GetIbanLetterValue(char letter)
        {
            return char.ToUpper(letter) - 'A' + 10;
        }

        private static bool ValidateMod97(string iban)
        {
            BigInteger number = BigInteger.Parse(iban);
            return number % 97 == 1;
        }
    }

}