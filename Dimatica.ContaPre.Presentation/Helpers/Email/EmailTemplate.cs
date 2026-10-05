namespace Dimatica.ContaPre.Presentation.Helpers.Email
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public static class EmailTemplate
    {
        // TODO --> elminar este metodo y usar GetEmailBodyDynamicParams
        public static string GetEmailBodyRecoveryCredentias(string templatePath, string userName, string userLogin, string password, string appUrl = "")
        {
            // Cargar la plantilla desde la ruta especificada
            string body = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath));
            return ReplaceTemplateParameters(body, new Dictionary<string, string>
            {
                { "UserName", userName },
                { "UserLogin", userLogin },
                { "Password", password },
                { "AppUrl", appUrl }
            });
        }

        public static string GetEmailBodyDynamicParams(string templatePath, Dictionary<string, string> parameters)
        {
            // Cargar la plantilla desde la ruta especificada
            string body = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath));

            return ReplaceTemplateParameters(body, parameters);
        }

        internal static string ReplaceTemplateParameters(string body, IDictionary<string, string> parameters)
        {
            // Reemplazar cada clave en el cuerpo de la plantilla
            foreach (var param in parameters)
            {
                string placeholder = $"{{{{{param.Key}}}}}"; // Crear el marcador de posición, por ejemplo {{UserName}}
                body = body.Replace(placeholder, param.Value ?? string.Empty); // Reemplazar con el valor o cadena vacía si es nulo
            }

            return body;
        }

    }

}
