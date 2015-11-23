namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class Blog : Page, IBlogView
    {
        private readonly BlogPresenter presenter;

        private UserViewModel owner;

        protected Blog()
        {
            this.presenter = new BlogPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var username = this.RouteData.Values["username"].ToString();
                this.presenter.Initialize(username);
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        public UserViewModel Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                this.owner = value;
                this.blogOwner.Text = string.Format("{0}'s blog", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Username));
                this.followers.DataSource = value.Followers;
                this.following.DataSource = value.Following;

                if (!this.Request.IsAuthenticated ||
                    this.User.Identity.GetUserName() == value.Username ||
                    value.Followers.Any(u => u.Id == this.User.Identity.GetUserId()))
                {
                    this.follow.Visible = false;
                }
            }
        }

        public List<PostViewModel> Posts
        {
            set
            {
                this.postsRepeater.DataSource = value;
                this.DataBind();
            }
        }

        protected void follow_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.Follow(this.User.Identity.GetUserId());
                this.follow.Visible = false;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "myKey",
                    string.Format("notificationModule.showErrorMessage('{0}')", ex.Message),
                    true);
            }
        }
    }
}