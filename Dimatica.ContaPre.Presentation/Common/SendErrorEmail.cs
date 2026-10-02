using Dimatica.ContaPre.Presentation.Helpers;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;

namespace Dimatica.ContaPre.Presentation.Common
{
    public class SendErrorEmail
    {

        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        public void SendEmail(Exception ex)
        {
            string sendEmailFlag = ConfigurationManager.AppSettings["SendErrorEmails"];
            string sendErrorEmailsUsers = ConfigurationManager.AppSettings["SendErrorEmailsUsers"];
            var appSettingsEmail = (NameValueCollection)ConfigurationManager.GetSection("appSettingsEmail");

            // Verificar que el flag está activado
            if (!string.IsNullOrEmpty(sendEmailFlag) && sendEmailFlag.ToLower() == "true")
            {
                try
                {
                    // Obtener la dirección de correo de "from"
                    string from = appSettingsEmail["smtp.from.noreply"];

                    // Validar que tenemos destinatarios
                    if (string.IsNullOrWhiteSpace(sendErrorEmailsUsers))
                    {
                        throw new Exception("No se han especificado direcciones de correo para recibir los errores.");
                    }

                    // Dividir los correos separados por comas y agregar a la lista de destinatarios
                    string[] recipients = sendErrorEmailsUsers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    string subject = "Error en la aplicación CONTAPRE";
                    string body = $"Detalles del error:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}";
                    bool isBodyHtml = false;

                    // Enviar el correo a cada destinatario
                    foreach (var recipient in recipients)
                    {
                        Email.SendEmail(from, recipient.Trim(), subject, body, isBodyHtml);
                    }
                }
                catch (Exception emailEx)
                {
                    _errorLogger.LogError(emailEx);
                }
            }
        }


    }
}
