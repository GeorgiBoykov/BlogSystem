namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Tag : Page, ITagView
    {
        private readonly TagPresenter presenter;

        protected Tag()
        {
            this.presenter = new TagPresenter(this);
        }

        public int Id { get; set; }

        public string Name
        {
            get
            {
                return this.tagName.Text;
            }
            set
            {
                this.tagName.Text = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var routeValues = this.RouteData.Values;
                this.presenter.Initialize(routeValues["name"].ToString());
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }
    }
}