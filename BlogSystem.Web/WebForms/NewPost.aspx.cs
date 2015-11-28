namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class NewPost : Page, INewPostView
    {
        private readonly NewPostPresenter presenter;

        protected NewPost()
        {
            this.presenter = new NewPostPresenter(this);
        }

        public string PostTitle
        {
            get
            {
                return this.Server.HtmlEncode(this.postTitle.Text);
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
                return this.Server.HtmlEncode(this.postTags.Text);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Request.IsAuthenticated)
            {
                this.Response.Redirect("/");
            }

            if (!this.IsPostBack)
            {
                try
                {
                    this.presenter.Initialize();
                }
                catch (Exception ex)
                {
                    this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
                }
            }
        }

        protected void submitPost_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.AddPost();
                this.Response.RedirectToRoute("User", new { username = this.User.Identity.Name });
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