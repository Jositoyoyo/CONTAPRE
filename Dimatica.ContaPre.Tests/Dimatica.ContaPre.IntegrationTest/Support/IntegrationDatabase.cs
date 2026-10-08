namespace Dimatica.ContaPre.IntegrationTest.Support
{
    using System;
    using System.IO;
    using System.Xml;

    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.DAL.Models;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    internal static class IntegrationDatabase
    {
        private const string ConnectionName = "ContaPreModel";
        private const string WebConfigEnvironmentVariable = "CONTAPRE_WEB_CONFIG_PATH";

        public static void Configure(object initializedDataContext)
        {
            Assert.IsNotNull(initializedDataContext, "Debe inicializarse primero el contexto DAL del servicio.");

            var connectionString = GetConnectionString();
            ContextModel.Db = new SqlDatabase(connectionString);
        }

        private static string GetConnectionString()
        {
            var configuredPath = Environment.GetEnvironmentVariable(WebConfigEnvironmentVariable);
            var webConfigPath = string.IsNullOrWhiteSpace(configuredPath)
                ? FindWebConfig()
                : configuredPath;

            if (string.IsNullOrWhiteSpace(webConfigPath) || !File.Exists(webConfigPath))
            {
                Assert.Inconclusive("No se encontró Presentation/Web.config. Define CONTAPRE_WEB_CONFIG_PATH para indicar su ubicación.");
            }

            var document = new XmlDocument();
            document.Load(webConfigPath);

            var connectionNode = document.SelectSingleNode(
                "/configuration/connectionStrings/add[@name='" + ConnectionName + "']");
            var connectionString = connectionNode?.Attributes?["connectionString"]?.Value;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Assert.Inconclusive("Web.config no contiene la conexión ContaPreModel requerida por las pruebas.");
            }

            return connectionString;
        }

        private static string FindWebConfig()
        {
            var startDirectories = new[]
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Environment.CurrentDirectory
            };

            foreach (var startDirectory in startDirectories)
            {
                var directory = new DirectoryInfo(startDirectory);
                while (directory != null)
                {
                    var candidate = Path.Combine(
                        directory.FullName,
                        "Dimatica.ContaPre.Presentation",
                        "Web.config");

                    if (File.Exists(candidate))
                    {
                        return candidate;
                    }

                    directory = directory.Parent;
                }
            }

            return null;
        }
    }
}
