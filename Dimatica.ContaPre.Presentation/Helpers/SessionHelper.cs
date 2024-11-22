namespace Dimatica.ContaPre.Presentation.Helpers
{
  
    public static class SessionHelper
    {
        #region Public Static Methods

        public static string Encrypt(string value)
        {
            return MyCryptography.Encrypt(value);
        }

        public static bool IsValidEncryption(string cipherText)
        {
            return MyCryptography.IsValidEncryption(cipherText);
        }

        public static string Decrypt(string value) 
        {
            return MyCryptography.Decrypt(value);
        }

        #endregion
    }
}