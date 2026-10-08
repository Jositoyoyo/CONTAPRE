using Dimatica.ContaPre.OL.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dimatica.ContaPre.UnitTest.OL.Business
{
    [TestClass]
    public class ResponseTests
    {
        [TestMethod]
        public void Properties_GetAssignedValues()
        {
            var method = new object();
            var response = new Response
            {
                ResponseMethod = method,
                ResponseCode = ResponseCode.Valid,
            };

            Assert.AreSame(method, response.ResponseMethod);
            Assert.AreEqual(ResponseCode.Valid, response.ResponseCode);
        }
    }
}
