using System.Collections.Generic;
using Dimatica.ContaPre.Presentation.Helpers.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.PresentationUnitTest.Helpers.Email
{
    [TestClass]
    public class EmailTemplateHelperTests
    {
        [TestMethod]
        public void ReplaceTemplateParameters_ReplacesAllValues()
        {
            var body = "{{UserName}}|{{UserLogin}}|{{Password}}|{{AppUrl}}";

            var result = EmailTemplate.ReplaceTemplateParameters(
                body,
                new Dictionary<string, string>
                {
                    { "UserName", "Ana" },
                    { "UserLogin", "ana.login" },
                    { "Password", "secret" },
                    { "AppUrl", "https://example.test" }
                });

            Assert.AreEqual("Ana|ana.login|secret|https://example.test", result);
        }

        [TestMethod]
        public void ReplaceTemplateParameters_ReplacesNullWithEmptyAndKeepsUnknownPlaceholders()
        {
            var result = EmailTemplate.ReplaceTemplateParameters(
                "{{Name}}|{{Empty}}|{{Unknown}}",
                new Dictionary<string, string>
                {
                    { "Name", "Ana" },
                    { "Empty", null }
                });

            Assert.AreEqual("Ana||{{Unknown}}", result);
        }
    }
}
