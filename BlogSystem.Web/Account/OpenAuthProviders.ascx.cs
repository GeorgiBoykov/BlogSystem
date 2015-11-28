namespace BlogSystem.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using BlogSystem.Web.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    public partial class OpenAuthProviders : UserControl
    {
        public string ReturnUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                var provider = this.Request.Form["provider"];
                if (provider == null)
                {
                    return;
                }
                // Request a redirect to the external login provider
                var redirectUrl =
                    this.ResolveUrl(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "~/Account/RegisterExternalLogin?{0}={1}&returnUrl={2}",
                            IdentityHelper.ProviderNameKey,
                            provider,
                            this.ReturnUrl));
                var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
                // Add xsrf verification when linking accounts
                if (this.Context.User.Identity.IsAuthenticated)
                {
                    properties.Dictionary[IdentityHelper.XsrfKey] = this.Context.User.Identity.GetUserId();
                }
                this.Context.GetOwinContext().Authentication.Challenge(properties, provider);
                this.Response.StatusCode = 401;
                this.Response.End();
            }
        }

        public IEnumerable<string> GetProviderNames()
        {
            return
                this.Context.GetOwinContext()
                    .Authentication.GetExternalAuthenticationTypes()
                    .Select(t => t.AuthenticationType);
        }
    }
}