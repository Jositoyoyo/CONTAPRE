using System;
using System.Web;
using Dimatica.ContaPre.Presentation.Common;
using Dimatica.ContaPre.Presentation.Helpers;
using Telerik.Web.UI.Diagram;

namespace Dimatica.ContaPre.Presentation
{
    public class Global : HttpApplication
    {
        private readonly LogUserRequestInfo _logUserRequestInfo = new LogUserRequestInfo();
        private readonly ErrorAppHandler _errorAppHandler = new ErrorAppHandler();

        void Application_Start(object sender, EventArgs e)
        {
            Console.WriteLine("Aplicación iniciada...");
        }

        void Application_Error(object sender, EventArgs e)
        {
            _errorAppHandler.HandleError(Server.GetLastError());
            Server.ClearError();
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            _logUserRequestInfo.requestInfo(Request);
        }
    }
}
