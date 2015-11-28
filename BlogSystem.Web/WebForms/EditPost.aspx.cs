namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Web.UI;

    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class EditPost : Page, IEditPostView
    {
        private readonly EditPostPresenter presenter;

        protected EditPost()
        {
            this.presenter = new EditPostPresenter(this);
        }

        public int PostId
        {
            get
            {
                return int.Parse(this.postId.Text);
            }
            set
            {
                this.postId.Text = value.ToString();
            }
        }

        public string PostTitle
        {
            get
            {
                return this.postTitle.Text;
            }
            set
            {
                this.postTitle.Text = this.Server.HtmlDecode(value);
            }
        }

        public string Content
        {
            get
            {
                return this.postContent.Text;
            }
            set
            {
                this.postContent.Text = value;
            }
        }

        public string Category
        {
            get
            {
                return this.postCategory.Text;
            }
            set
            {
                this.postCategory.Text = value;
            }
        }

        public string AuthorId { get; set; }

        public string Tags
        {
            get
            {
                return this.postTags.Text;
            }
            set
            {
                this.postTags.Text = this.Server.HtmlDecode(value);
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
                    var routeValues = this.RouteData.Values;
                    this.presenter.Initialize(int.Parse(routeValues["id"].ToString()));
                }
                catch (Exception ex)
                {
                    this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
                }
            }
        }

        protected void submitChanges_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.EditPost(
                    int.Parse(this.postId.Text),
                    this.User.Identity.GetUserId(),
                    this.Server.HtmlEncode(this.postTitle.Text),
                    this.postContent.Text);

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