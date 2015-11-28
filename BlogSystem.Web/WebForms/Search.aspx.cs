namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Search : Page, ISearchView
    {
        private readonly SearchPresenter presenter;

        protected Search()
        {
            this.presenter = new SearchPresenter(this);
        }

        public List<PostViewModel> PostResults
        {
            get
            {
                return this.postsResults.DataSource as List<PostViewModel>;
            }
            set
            {
                this.postsResults.DataSource = value;
                this.DataBind();
            }
        }

        public List<UserViewModel> UserResults
        {
            get
            {
                return this.usersResults.DataSource as List<UserViewModel>;
            }
            set
            {
                this.usersResults.DataSource = value;
                this.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.presenter.Initialize(this.RouteData.Values["term"].ToString());
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }
    }
}