using System.Linq;
using Dimatica.ContaPre.Presentation.Helpers.SiteData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimatica.ContaPre.UnitTest.Presentation.Helpers;

namespace Dimatica.ContaPre.UnitTest.Presentation.Helpers.SiteData
{
    [TestClass]
    public class SiteDataHelperTests
    {
        [TestMethod]
        public void GetSiteDataItems_LocalhostIncludesDevelopmentMenu()
        {
            using (WebTestContext.Create("http://localhost/Default.aspx"))
            {
                var items = SiteDataHelper.GetSiteDataItems();

                Assert.IsTrue(items.Any(item => item.Text == "Presupuestos"));
                Assert.IsTrue(items.Any(item => item.Text == "Development"));
            }
        }

        [TestMethod]
        public void GetSiteDataItems_NonLocalHostExcludesDevelopmentMenu()
        {
            using (WebTestContext.Create("https://production.example.test/Default.aspx"))
            {
                var items = SiteDataHelper.GetSiteDataItems();

                Assert.IsTrue(items.Any(item => item.Text == "Presupuestos"));
                Assert.IsFalse(items.Any(item => item.Text == "Development"));
            }
        }
    }
}
