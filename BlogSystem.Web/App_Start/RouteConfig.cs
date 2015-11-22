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
            routes.MapPageRoute("CustomErrorPage", "CustomErrorPage", "~/WebForms/CustomErrorPage.aspx");

            routes.MapPageRoute("User", "{username}", "~/WebForms/Blog.aspx",false,null,
                new RouteValueDictionary { {"username", "^((?!Account|Contact|About|NewPost|Search).)*$" } });

            routes.MapPageRoute("Post", "{username}/{slug}", "~/WebForms/Post.aspx", false, null,
                new RouteValueDictionary { { "username", "^((?!Account|Search).)*$" } });

            routes.MapPageRoute("Tag", "tags/show/{name}", "~/WebForms/Tag.aspx");

            routes.MapPageRoute("Category", "categories/show/{name}", "~/WebForms/Category.aspx");

            routes.MapPageRoute("NewPost", "NewPost", "~/WebForms/NewPost.aspx");

            routes.MapPageRoute("Search", "search/{term}", "~/WebForms/Search.aspx");

            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
