namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Category : Page, ICategoryView
    {
        private readonly CategoryPresenter presenter;

        protected Category()
        {
            this.presenter = new CategoryPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var routeValues = this.RouteData.Values;
                int page = int.TryParse(this.Request.QueryString["page"], out page) ? page : 1;
                this.presenter.Initialize(routeValues["name"].ToString(), page);
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        public int Id { get; set; }

        public string Name
        {
            get
            {
                return this.categoryName.Text;
            }
            set
            {
                this.categoryName.Text = value;
            }
        }

        public List<PostViewModel> Posts
        {
            get
            {
                return this.postsRepeater.DataSource as List<PostViewModel>;
            }
            set
            {
                this.postsRepeater.DataSource = value;
                this.DataBind();
            }
        }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}