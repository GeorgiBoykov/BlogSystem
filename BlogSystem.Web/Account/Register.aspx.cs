namespace BlogSystem.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using BlogSystem.Models;
    using BlogSystem.Web.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = this.Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new User { UserName = this.Username.Text, Email = this.Email.Text };
            var result = manager.Create(user, this.Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn(user, false, false);
                IdentityHelper.RedirectToReturnUrl(this.Request.QueryString["ReturnUrl"], this.Response);
            }
            else
            {
                this.ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}