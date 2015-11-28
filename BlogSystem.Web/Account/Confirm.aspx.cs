namespace BlogSystem.Web.Account
{
    using System;
    using System.Web;
    using System.Web.UI;

    using BlogSystem.Web.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public partial class Confirm : Page
    {
        protected string StatusMessage { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var code = IdentityHelper.GetCodeFromRequest(this.Request);
            var userId = IdentityHelper.GetUserIdFromRequest(this.Request);
            if (code != null && userId != null)
            {
                var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = manager.ConfirmEmail(userId, code);
                if (result.Succeeded)
                {
                    this.successPanel.Visible = true;
                    return;
                }
            }
            this.successPanel.Visible = false;
            this.errorPanel.Visible = true;
        }
    }
}