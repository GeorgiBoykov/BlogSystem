namespace BlogSystem.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Blog : Page, IBlogView
    {
        private readonly BlogPresenter presenter;

        protected Blog()
        {
            this.presenter = new BlogPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                var username = this.RouteData.Values["username"].ToString();
                this.presenter.Initialize(username);
            //}
            //catch (Exception ex)
            //{
            //    this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            //}
        }

        public List<PostViewModel> Posts
        {
            set
            {
                this.postsRepeater.DataSource = value;
                this.DataBind();
            }
        }
    }
}