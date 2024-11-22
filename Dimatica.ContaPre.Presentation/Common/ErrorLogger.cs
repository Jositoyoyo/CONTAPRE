using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;

namespace Dimatica.ContaPre.Presentation.Common
{
    public class ErrorLogger
    {
        private readonly string logPath;

        public ErrorLogger(string relativeLogPath = "~/Logs/ErrorLog.txt")
        {
            if (HttpContext.Current == null)
            {
                logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "ErrorLog.txt");
            }
            else
            {
                logPath = HttpContext.Current.Server.MapPath(relativeLogPath);
            }
            EnsureLogDirectoryExists();
        }

        private void EnsureLogDirectoryExists()
        {
            string logDirectory = Path.GetDirectoryName(logPath);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);

                // Establecer permisos de lectura y escritura para todos los usuarios
                DirectorySecurity security = Directory.GetAccessControl(logDirectory);
                security.AddAccessRule(new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow));

                Directory.SetAccessControl(logDirectory, security);
            }

            // Crear el archivo si no existe
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();
            }
        }

        public void LogError(Exception ex)
        {
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
                        writer.WriteLine("Fecha: " + DateTime.Now.ToString());
                        writer.WriteLine("Excepción: " + ex.ToString());
                        writer.WriteLine();
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
                    // Manejo adicional de errores, opcional
                    Console.WriteLine("Error al escribir en el log: " + generalEx.Message);
                    break;
                }
            }
        }
    }
}
