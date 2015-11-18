using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace BlogSystem.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("CustomErrorPage", "CustomErrorPage", "~/CustomErrorPage.aspx");

            routes.MapPageRoute("User", "{username}", "~/Blog.aspx",false,null,
                new RouteValueDictionary { {"username", "^((?!Account|Contact|About|NewPost).)*$" } });

            routes.MapPageRoute("Post", "{username}/{slug}", "~/Post.aspx", false, null,
                new RouteValueDictionary { { "username", "^((?!Account).)*$" } });

            routes.MapPageRoute("Tag", "tags/show/{name}", "~/Tag.aspx");

            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
