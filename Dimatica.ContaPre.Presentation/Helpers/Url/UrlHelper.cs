namespace Dimatica.ContaPre.Presentation.Helpers.Url
{

    /*
     * utilizar de la siguiente manera para obtener una URL absoluta:
     * string logoutUrl = HttpContext.Current.Request.GetAbsoluteUrl("~/Account/Login.aspx?SignOut=1");
     */
    using System;
    using System.Web;

    public static class UrlHelper
    {
        public static string GetAbsoluteUrl(this HttpRequest request, string relativeUrl)
        {
            Uri uri = new Uri(request.Url, VirtualPathUtility.ToAbsolute(relativeUrl));
            return uri.ToString();
        }
    }

}