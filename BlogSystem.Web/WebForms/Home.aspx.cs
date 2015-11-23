namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class Home : System.Web.UI.Page, IHomeView
    {
        private readonly HomePresenter presenter;

        protected Home()
        {
            this.presenter = new HomePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.presenter.Initialize(this.User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        public List<PostViewModel> LatestPosts
        {
            get
            {
                return this.latestPosts.DataSource as List<PostViewModel>;
            }
            set
            {
                this.latestPosts.DataSource = value;
                this.DataBind();
            }
        }

        public List<CommentViewModel> LatestComments
        {
            get
            {
                return this.latestComments.DataSource as List<CommentViewModel>;
            }
            set
            {
                this.latestComments.DataSource = value;
                this.DataBind();
            }
        }

        public List<TagViewModel> FamousTags
        {
            get
            {
                return this.famousTags.DataSource as List<TagViewModel>;
            }
            set
            {
                this.famousTags.DataSource = value;
                this.DataBind();
            }
        }
    }
}