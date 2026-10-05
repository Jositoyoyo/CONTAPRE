using System.Web;
using Dimatica.ContaPre.Presentation.Helpers.Url;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers
{
    [TestClass]
    public class UrlHelperTests
    {
        [TestMethod]
        public void GetAbsoluteUrl_ResolvesApplicationRelativePath()
        {
            using (WebTestContext.Create("https://example.test/app/Default.aspx"))
            {
                var request = HttpContext.Current.Request;
                var absoluteUrl = request.GetAbsoluteUrl("/Account/Login.aspx?SignOut=1");

                Assert.AreEqual("https://example.test/Account/Login.aspx?SignOut=1", absoluteUrl);
            }
        }
    }
}
