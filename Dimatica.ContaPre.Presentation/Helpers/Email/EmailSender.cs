namespace Dimatica.ContaPre.Presentation.Helpers.Email
{

    using System.Net;
    using System.Net.Mail;
    using System.Configuration;

    public static class EmailSender
    {

        public static void SendEmail(string fromAddress, string to, string subject, string body, bool isBodyHtml)
        {

            var appEmailSettings = ConfigurationManager.GetSection("appSettingsEmail") as System.Collections.Specialized.NameValueCollection;

            if (appEmailSettings == null)
            {
                throw new ConfigurationErrorsException("Configuracion del envio de correos no encontrada en el archivo de configuración");
            }

            string host         = appEmailSettings["smtp.network.host"];
            int port            = int.Parse(appEmailSettings["smtp.network.port"]);
            string userName     = appEmailSettings["smtp.network.userName"];
            string password     = appEmailSettings["smtp.network.password"];
            bool enableSsl      = bool.Parse(appEmailSettings["smtp.network.enableSsl"]);

            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(fromAddress);
                message.To.Add(new MailAddress(to));
                message.Subject     = subject;
                message.Body        = body;
                message.IsBodyHtml  = isBodyHtml;

                using (SmtpClient smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.Credentials = new NetworkCredential(userName, password);
                    smtpClient.EnableSsl   = enableSsl;
                    smtpClient.Send(message);
                }
            }
        }
    }
}
