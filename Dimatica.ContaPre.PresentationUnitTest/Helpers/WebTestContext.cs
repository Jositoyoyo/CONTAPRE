using System;
using System.IO;
using System.Web;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers
{
    internal sealed class WebTestContext : IDisposable
    {
        private readonly HttpContext previousContext;

        private WebTestContext(HttpContext context)
        {
            previousContext = HttpContext.Current;
            HttpContext.Current = context;
        }

        public static WebTestContext Create(string url)
        {
            var request = new HttpRequest("Default.aspx", url, string.Empty);
            var response = new HttpResponse(new StringWriter());
            return new WebTestContext(new HttpContext(request, response));
        }

        public void Dispose()
        {
            HttpContext.Current = previousContext;
        }
    }
}
