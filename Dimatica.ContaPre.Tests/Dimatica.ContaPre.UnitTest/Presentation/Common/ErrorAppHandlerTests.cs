using System;
using System.Net;
using System.Text;
using System.Web;
using Dimatica.ContaPre.Presentation.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.Presentation.Common
{
    [TestClass]
    public class ErrorAppHandlerTests
    {
        [TestMethod]
        public void WriteErrorResponse_WhenTraceEnabled_ReturnsEncodedDetailsWithoutRedirect()
        {
            Exception exception = CreateException("<script>alert('error')</script>");

            var response = new TestHttpResponse();
            ErrorAppHandler.WriteErrorResponse(response, exception, false, true);

            Assert.AreEqual(500, response.StatusCode);
            Assert.AreEqual("text/html; charset=utf-8", response.ContentType);
            Assert.IsTrue(response.TrySkipIisCustomErrors);
            Assert.IsNull(response.RedirectUrl);
            StringAssert.Contains(response.Body, "&lt;script&gt;alert(&#39;error&#39;)&lt;/script&gt;");
            StringAssert.Contains(response.Body, "ErrorAppHandlerTests");
            Assert.IsFalse(response.Body.Contains("<script>alert('error')</script>"));
        }

        [TestMethod]
        public void WriteErrorResponse_WhenTraceDisabled_RedirectsToGeneralError()
        {
            var response = new TestHttpResponse();
            ErrorAppHandler.WriteErrorResponse(response, new Exception("hidden detail"), false, false);

            Assert.AreEqual((int)HttpStatusCode.Redirect, response.StatusCode);
            Assert.AreEqual("~/ErrorPages/GeneralError.html", response.RedirectUrl);
            Assert.IsFalse(response.Body.Contains("hidden detail"));
        }

        [TestMethod]
        public void WriteErrorResponse_ForAjaxWithTraceEnabled_PreservesJsonDetails()
        {
            Exception exception = CreateException("ajax error");

            var response = new TestHttpResponse();
            ErrorAppHandler.WriteErrorResponse(response, exception, true, true);

            Assert.AreEqual(500, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
            StringAssert.Contains(response.Body, "\"error\":\"ajax error\"");
            StringAssert.Contains(response.Body, "\"stackTrace\":");
        }

        private static Exception CreateException(string message)
        {
            try
            {
                throw new InvalidOperationException(message);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        private sealed class TestHttpResponse : HttpResponseBase
        {
            private readonly StringBuilder body = new StringBuilder();

            public string Body => this.body.ToString();

            public string RedirectUrl { get; private set; }

            public override string ContentType { get; set; }

            public override int StatusCode { get; set; }

            public override bool TrySkipIisCustomErrors { get; set; }

            public override void Clear()
            {
                this.body.Clear();
            }

            public override void Write(string value)
            {
                this.body.Append(value);
            }

            public override void Redirect(string url, bool endResponse)
            {
                this.RedirectUrl = url;
                this.StatusCode = (int)HttpStatusCode.Redirect;
            }
        }
    }
}
