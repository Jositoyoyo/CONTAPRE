namespace Dimatica.ContaPre.Presentation.Helpers.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class MyCryptography
    {

        #region Public Static Methods

        private static readonly string EncryptionKey = "tF6K0AUk5JRIhyoIquahPzuPQ=";

        public static string Encrypt(string clearText)
        {

            if (string.IsNullOrEmpty(clearText))
            {
                return string.Empty;
            }

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static bool IsValidEncryption(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return false;
            }

            // Intentar desencriptar el texto cifrado
            try
            {
                // Si la desencriptación es exitosa, el cifrado es válido
                string decryptedText = Decrypt(cipherText);
                return true;
            }
            catch (CryptographicException)
            {
                // Si hay una excepción, el cifrado no es válido
                return false;
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes;

            try
            {
                cipherBytes = Convert.FromBase64String(cipherText);
            }
            catch (FormatException)
            {
                throw new CryptographicException("El texto cifrado no tiene un formato válido.");
            }

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        try
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.FlushFinalBlock();  // Asegurarse de que se procesa todo el bloque
                        }
                        catch (CryptographicException)
                        {
                            throw new CryptographicException("Error al descifrar los datos. Asegúrate de que la clave y el IV sean correctos. Pruebe a resetear los datos");
                        }
                    }

                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }

        #endregion

    }


}