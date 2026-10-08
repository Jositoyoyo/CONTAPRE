using Dimatica.ContaPre.OL.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.OL.Business
{
    [TestClass]
    public class SiteDataItemTests
    {
        [TestMethod]
        public void Constructor_SetsIdentifiersTextAndPath()
        {
            var item = new SiteDataItem(12, 4, "Tesorería", "/Treasury.aspx");

            Assert.AreEqual(12, item.Id);
            Assert.AreEqual(4, item.ParentId);
            Assert.AreEqual("Tesorería", item.Text);
            Assert.AreEqual("/Treasury.aspx", item.Path);
        }

        [TestMethod]
        public void Constructor_WithoutPath_UsesEmptyPath()
        {
            var item = new SiteDataItem(1, 0, "Inicio");

            Assert.AreEqual(string.Empty, item.Path);
        }

        [TestMethod]
        public void Properties_CanBeUpdatedAfterConstruction()
        {
            var item = new SiteDataItem(1, 0, "Inicio", "/Default.aspx")
            {
                Id = 8,
                ParentId = 3,
                Text = "Tesorería",
                Path = "/Treasury.aspx",
            };

            Assert.AreEqual(8, item.Id);
            Assert.AreEqual(3, item.ParentId);
            Assert.AreEqual("Tesorería", item.Text);
            Assert.AreEqual("/Treasury.aspx", item.Path);
        }

        [TestMethod]
        public void TextAndPath_WhenNull_ReturnEmptyStrings()
        {
            var item = new SiteDataItem(1, 0, "Inicio", "/Default.aspx")
            {
                Text = null,
                Path = null,
            };

            Assert.AreEqual(string.Empty, item.Text);
            Assert.AreEqual(string.Empty, item.Path);
        }
    }
}
