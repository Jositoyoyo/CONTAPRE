namespace Dimatica.ContaPre.Presentation.DataValidation
{

    using System.Text.RegularExpressions;

    public class EmailValidator
    {
        public static bool IsValidCorporateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\.)?[a-zA-Z]+\.)?(uimp\.es)$";
            return Regex.IsMatch(email, pattern);
        }
    }

}