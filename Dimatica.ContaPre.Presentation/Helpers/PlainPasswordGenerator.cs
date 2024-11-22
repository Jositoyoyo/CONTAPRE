namespace Dimatica.ContaPre.Presentation.Helpers
{
    using System;
    using System.Text;

    public class PlainPasswordGenerator
    {
        private static readonly Random random = new Random();

        private const string chars      = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        private const string digitChars = "0123456789";
        private const string allChars     = upperChars + lowerChars + digitChars;

        public static string simpleGeneratePassword(int length)
        {
            var password = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }
            return password.ToString();
        }
        public static string GeneratePassword(int length = 8)
        {
            if (length < 8)
            {
                length = 8; // forzamos a que sea minimo 8 en vez de lanzar una excepcion
            }

            var password = new StringBuilder();

            // Asegurar que contiene al menos un carácter mayúscula y un dígito
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(digitChars[random.Next(digitChars.Length)]);

            // Completar el resto de la contraseña con caracteres aleatorios
            for (int i = 2; i < length; i++) // Empieza en 2 porque ya añadimos 2 caracteres
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Mezclar los caracteres para evitar que los primeros siempre sean upper, lower, etc.
            return ShufflePassword(password.ToString());
        }

        private static string ShufflePassword(string password)
        {
            char[] array = password.ToCharArray();
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new string(array);
        }
    }
}
