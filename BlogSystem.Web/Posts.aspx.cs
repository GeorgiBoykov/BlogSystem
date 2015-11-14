namespace BlogSystem.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Posts : Page, IPostsView
    {
        private readonly PostsPresenter presenter;

        protected Posts()
        {
            this.presenter = new PostsPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.presenter.Initialize();
        }

        public List<PostView> PostItems
        {
            set
            {
                this.postsRepeater.DataSource = value;
                this.DataBind();
            }
        }
    }
}