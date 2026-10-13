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

        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = BaseAddress
        };

        [TestMethod]
        public async Task LoginPage_ReturnsSuccessAndExpectedContent()
        {
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync("/Views/Account/Login.aspx");
            }
            catch (HttpRequestException exception)
            {
                Assert.Fail(
                    "No se pudo conectar con ContaPre en {0}. Inicia la aplicación local o configura CONTAPRE_BASE_URL. Detalle: {1}",
                    BaseAddress,
                    exception.Message);
                return;
            }

            using (response)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var html = await response.Content.ReadAsStringAsync();
                StringAssert.Contains(html, "Contabilidad Presupuestaria");
            }
        }
    }
}
