using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Dimatica.ContaPre.Presentation.Common
{
    public class LogUserRequestInfo
    {
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        public void requestInfo(HttpRequest Request)
        {
            string logUserRequestInfo = ConfigurationManager.AppSettings["LogUserRequestInfo"];

            if (logUserRequestInfo != null && logUserRequestInfo.ToLower() == "true")
            {
                try
                {
                    string logPath = HttpContext.Current.Server.MapPath("~/Logs/UserRequestLog.txt");

                    // Intentar crear el archivo si no existe
                    if (!File.Exists(logPath))
                    {
                        File.Create(logPath).Dispose();
                    }

                    string userIp = Request.UserHostAddress;
                    string userAgent = Request.UserAgent;
                    string userBrowser = Request.Browser.Browser + " " + Request.Browser.Version;
                    string userUrl = Request.Url.AbsoluteUri;
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    int maxRetries = 3;
                    int retryDelay = 100; // Milisegundos

                    for (int i = 0; i < maxRetries; i++)
                    {
                        try
                        {
                            // Abrir el archivo con FileStream para asegurar exclusividad
                            using (FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.None))
                            using (StreamWriter writer = new StreamWriter(fs))
                            {
                                writer.WriteLine($"Fecha: {timestamp}");
                                writer.WriteLine($"IP: {userIp}");
                                writer.WriteLine($"Navegador: {userBrowser}");
                                writer.WriteLine($"User-Agent: {userAgent}");
                                writer.WriteLine($"URL solicitada: {userUrl}");
                                writer.WriteLine("----------------------------");
                            }
                            break; // Salir del ciclo si el proceso de escritura es exitoso
                        }
                        catch (IOException)
                        {
                            // Esperar antes de reintentar si el archivo está en uso
                            System.Threading.Thread.Sleep(retryDelay);
                        }
                        catch (Exception generalEx)
                        {
                            _errorLogger.LogError(generalEx);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _errorLogger.LogError(ex);
                }
            }
        }



    }
}
