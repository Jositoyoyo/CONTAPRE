namespace Dimatica.ContaPre.HttpTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("HttpIntegration")]
    [DoNotParallelize]
    public class AccountNavigationTests
    {
        private static readonly Uri BaseAddress = new Uri(
            Environment.GetEnvironmentVariable("CONTAPRE_BASE_URL")
            ?? "http://localhost:54234");

        private static readonly HttpClient Client = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false
        })
        {
            BaseAddress = BaseAddress
        };

        [TestMethod]
        public async Task LoginPage_ReturnsSuccessAndExpectedContent()
        {
            using (var response = await GetResponseAsync("/Views/Account/Login.aspx"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var html = await response.Content.ReadAsStringAsync();
                StringAssert.Contains(html, "Contabilidad Presupuestaria");
                StringAssert.Contains(html, "Usuario:");
                StringAssert.Contains(html, "Password:");
                StringAssert.Contains(html, "Entrar");
                StringAssert.Contains(html, "¿Olvidaste tu contraseña?");
                StringAssert.Contains(html, "ForgotPassword.aspx");
            }
        }

        [TestMethod]
        public async Task ForgotPasswordPage_ReturnsSuccessAndLoginNavigation()
        {
            using (var response = await GetResponseAsync("/Views/Account/ForgotPassword.aspx"))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var html = await response.Content.ReadAsStringAsync();
                StringAssert.Contains(html, "Por favor, introduce tu nombre de usuario");
                StringAssert.Contains(html, "Enviar");
                StringAssert.Contains(html, "href=\"Login.aspx\"");
                StringAssert.Contains(html, "Volver");
            }
        }

        [TestMethod]
        public async Task DefaultPage_RedirectsToLogin()
        {
            using (var response = await GetResponseAsync("/Default.aspx"))
            {
                Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
                Assert.IsNotNull(response.Headers.Location, "La redirección debe indicar la página de inicio de sesión.");

                var location = response.Headers.Location.IsAbsoluteUri
                    ? response.Headers.Location
                    : new Uri(BaseAddress, response.Headers.Location);

                Assert.AreEqual("/Views/Account/Login.aspx", location.AbsolutePath);
            }
        }

        private static async Task<HttpResponseMessage> GetResponseAsync(string path)
        {
            try
            {
                return await Client.GetAsync(path);
            }
            catch (HttpRequestException exception)
            {
                Assert.Fail(
                    "No se pudo conectar con ContaPre en {0}. Inicia la aplicación local o configura CONTAPRE_BASE_URL. Detalle: {1}",
                    BaseAddress,
                    exception.Message);
                throw;
            }
        }
    }
}
