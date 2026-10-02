namespace Dimatica.ContaPre.Presentation.Helpers
{
    #region NameSpaces

    using System.IO;
    using System.Web;
    using Newtonsoft.Json.Linq;

    #endregion

    internal sealed class ApplicationVersionInfo
    {
        public string Version { get; set; }

        public string ReleaseDate { get; set; }
    }

    internal static class ApplicationVersionLoader
    {
        #region Internal Static Methods

        internal static ApplicationVersionInfo LoadApplicationVersion()
        {
            var applicationVersion = new ApplicationVersionInfo
            {
                Version = string.Empty,
                ReleaseDate = string.Empty
            };

            try
            {
                var context = HttpContext.Current;
                if (context == null || context.Server == null)
                {
                    return applicationVersion;
                }

                var versionPath = context.Server.MapPath("~/JSON.json");
                var versionData = JObject.Parse(File.ReadAllText(versionPath));
                var version = versionData["version"];
                var releaseDate = versionData["releaseDate"];

                if (version != null && version.Type == JTokenType.String)
                {
                    applicationVersion.Version = version.Value<string>();
                }

                if (releaseDate != null && releaseDate.Type == JTokenType.String)
                {
                    applicationVersion.ReleaseDate = releaseDate.Value<string>();
                }
            }
            catch
            {
                return new ApplicationVersionInfo
                {
                    Version = string.Empty,
                    ReleaseDate = string.Empty
                };
            }

            return applicationVersion;
        }

        #endregion
    }
}
