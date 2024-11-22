using System;
using System.Configuration;
using System.Web;

namespace Dimatica.ContaPre.Presentation.Common
{
    public class ErrorAppHandler
    {
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private readonly SendErrorEmail _sendErrorEmail = new SendErrorEmail();

        public void HandleError(Exception exception)
        {
            // Registrar el error
            _errorLogger.LogError(exception);

            string enableErrorTrace = ConfigurationManager.AppSettings["enableErrorTrace"];
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;

            // Verificar si la solicitud es AJAX
            bool isAjaxRequest = request.Headers["X-Requested-With"] == "XMLHttpRequest";

            // Si es una solicitud AJAX, retorna JSON en lugar de HTML
            if (isAjaxRequest)
            {
                response.ContentType = "application/json; charset=utf-8";
                response.StatusCode = 500; // Código de error interno del servidor

                if (!string.IsNullOrEmpty(enableErrorTrace) && enableErrorTrace.ToLower() == "true")
                {
                    // Enviar la traza completa en JSON
                    response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        error = exception.Message,
                        stackTrace = exception.StackTrace
                    }));
                }
                else
                {
                    // Enviar solo un mensaje de error genérico
                    response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        error = "Ocurrió un error inesperado. Por favor, inténtelo de nuevo más tarde."
                    }));
                }
            }
            else
            {
                // Manejo estándar para solicitudes normales (no AJAX)
                if (!string.IsNullOrEmpty(enableErrorTrace) && enableErrorTrace.ToLower() == "true")
                {
                    response.Write($@"
                <html>
                <head>
                    <title>Error del servidor</title>
                </head>
                <body>
                    <h1>Ocurrió un error inesperado</h1>
                    <p>{exception.Message}</p>
                    <p>{exception.StackTrace.Replace("\n", "<br/>")}</p>
                </body>
                </html>");
                }
                else
                {
                    response.Redirect("~/ErrorPages/GeneralError.html");
                }
            }

            // Enviar correo con el error
            _sendErrorEmail.SendEmail(exception);
            response.End();
        }

    }

}
