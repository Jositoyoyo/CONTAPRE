using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dimatica.ContaPre.Helpers
{
    public class IdentificationValidator
    {

        #region public methods
        public bool ValidateDocument(string document)
        {
            var type  = this.SpainIdType(document);
            var valid = false;

            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }

            switch (type)
            {
                case "dni":
                    valid = this.ValidateNIF(document);
                    break;
                case "cif":
                    valid = this.ValidateCIF(document);
                    break;
                case "nie":
                    valid = this.ValidNIE(document);
                    break;
            }

            return valid;
        }

        #endregion

        #region private methods

        private string SpainIdType(string nif)
        {
            var dniRegex = new Regex(@"(\d{8})([A-Z])$");

            if (dniRegex.IsMatch(nif))
            {
                return "dni";
            }

            var cifRegex = new Regex(@"([ABCDEFGHJKLMNPQRSUVW])(\d{7})([0-9A-J])$");

            if (cifRegex.IsMatch(nif))
            {
                return "cif";
            }

            var nieRegex = new Regex(@"[XYZ]\d{7,8}[A-Z]$");

            if (nieRegex.IsMatch(nif))
            {
                return "nie";
            }

            return string.Empty;
        }

        public bool ValidateNIF(string dni)
        {
            if (dni.Length != 9)
            {
                // El DNI debería tener exactamente 8 números y 1 letra.
                return false;
            }

            // Extraer la parte numérica (primeros 8 caracteres).
            string dniNumberPart = dni.Substring(0, 8);

            if (!int.TryParse(dniNumberPart, out int dniNumber))
            {
                // Si la parte numérica no es válida, retornar false.
                return false;
            }

            // Obtener la letra de control esperada.
            var dniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";
            char expectedLetter = dniLetters[dniNumber % 23];

            // Comparar la letra esperada con la letra proporcionada.
            char providedLetter = dni[8];
            return expectedLetter == providedLetter;
        }

        public bool ValidateCIF(string cif)
        {
            if (string.IsNullOrEmpty(cif) || cif.Length != 9)
                return false;

            cif = cif.ToUpper();
            char firstChar = cif[0];
            string digits = cif.Substring(1, 7);
            char controlChar = cif[8];

            if (!"ABCDEFGHJNPQRSUVW".Contains(firstChar))
                return false;

            if (!int.TryParse(digits, out _))
                return false;

            int evenSum = 0, oddSum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i] - '0';
                if (i % 2 == 0)  // Odd positions (0-indexed)
                {
                    int doubleDigit = digit * 2;
                    oddSum += doubleDigit > 9 ? doubleDigit - 9 : doubleDigit;
                }
                else  // Even positions
                {
                    evenSum += digit;
                }
            }

            int totalSum = evenSum + oddSum;
            int controlDigit = (10 - (totalSum % 10)) % 10;
            char expectedControl = controlDigit.ToString()[0];

            // Check if the CIF starts with N, P, Q, S, or W, which require a letter as control
            if ("NPQSW".Contains(firstChar))
            {
                return controlChar == "JABCDEFGHI"[controlDigit];
            }

            // For other types, the control can be a digit or letter
            return controlChar == expectedControl || controlChar == "JABCDEFGHI"[controlDigit];
        }

        public bool ValidNIE(string nie)
        {
            var niePrefix = nie[0];

            switch (niePrefix)
            {
                case 'X':
                    niePrefix = '0';

                    break;
                case 'Y':
                    niePrefix = '1';

                    break;
                case 'Z':
                    niePrefix = '2';

                    break;
            }

            return this.ValidateNIF(niePrefix + nie.Substring(1));
        }

        #endregion

    }
}
