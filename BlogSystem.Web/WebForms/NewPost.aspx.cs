namespace BlogSystem.Web
{
    using System;
    using System.Collections.Generic;

    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class NewPost : System.Web.UI.Page, INewPostView
    {
        private readonly NewPostPresenter presenter;

        protected NewPost()
        {
            this.presenter = new NewPostPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.presenter.Initialize();
            }
        }

        public string PostTitle
        {
            get
            {
                return this.postTitle.Text;
            }
        }

        public string Content
        {
            get
            {
                return this.postContent.Text;
            }
        }

        public string Category
        {
            get
            {
                return this.postCategory.SelectedValue;
            }
        }

        public List<string> CategoriesList
        {
            set
            {
                this.postCategory.DataSource = value;
                this.DataBind();
            }
        }

        public string AuthorId
        {
            get
            {
                return this.User.Identity.GetUserId();
            }
        }

        public string Tags
        {
            get
            {
                return this.postTags.Text;
            }
        }

        protected void submitPost_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.AddPost();
                this.Response.RedirectToRoute("User", new { username = this.User.Identity.Name });
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }
    }
}