using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Dimatica.ContaPre.Presentation
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
