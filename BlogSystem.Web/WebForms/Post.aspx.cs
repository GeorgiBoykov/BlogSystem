namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Utilities;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class Post : Page, IPostView
    {
        private readonly PostPresenter presenter;

        private AuthorViewModel author;

        private CategoryViewModel category;

        private int id;

        private List<LikeViewModel> likes;

        protected Post()
        {
            this.presenter = new PostPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var routeValues = this.RouteData.Values;
                this.presenter.Initialize(routeValues["username"].ToString(), routeValues["slug"].ToString());
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.edit.NavigateUrl = string.Format("../edit/{0}", value);
                if (this.User.Identity.GetUserId() != this.Author.Id)
                {
                    this.edit.Visible = false;
                    this.delete.Visible = false;
                }
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
                this.postTitle.Text = value;
            }
        }

        public string Content
        {
            set
            {
                this.content.Text = value;
            }
        }

        public string Slug { get; set; }

        public CategoryViewModel Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
                this.categoryName.Text = value.Name;
                this.categoryName.NavigateUrl = string.Format("../categories/show/{0}", value.Name);
            }
        }

        public AuthorViewModel Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
                this.authorUsername.Text = value.UserName;
            }
        }

        public DateTime DateCreated
        {
            set
            {
                this.dateCreated.Text = value.ToShortDateString();
            }
        }

        public List<CommentViewModel> Comments
        {
            get
            {
                return this.comments.DataSource as List<CommentViewModel>;
            }
            set
            {
                this.comments.DataSource = value;
                this.DataBind();
            }
        }

        public List<TagViewModel> Tags
        {
            set
            {
                this.tags.DataSource = value;
                this.DataBind();
            }
        }

        public List<LikeViewModel> Likes
        {
            get
            {
                return this.likes;
            }

            set
            {
                this.likes = value;
                this.likeBtn.Text = string.Format("Like: {0}", value.Count);

                if (this.User.Identity.Name == this.Author.UserName
                    || value.Any(l => l.UserId == this.User.Identity.GetUserId())
                    || value.Any(l => l.IpAddress == WebExtensions.GetUserIp(this.Request)))
                {
                    this.likeBtn.Attributes.Add("disabled", "");
                }
            }
        }

        protected void addCommentBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.AddComment(
                    this.Server.HtmlEncode(this.addCommentAuthor.Text),
                    this.Server.HtmlEncode(this.addCommentContent.Text));

                this.DataBind();
                this.commentsPanel.Update();
                this.addCommentAuthor.Text = string.Empty;
                this.addCommentContent.Text = string.Empty;
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

        protected void likeBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.LikePost(this.User.Identity.GetUserId(), WebExtensions.GetUserIp(this.Request));
                this.likeBtn.Text = string.Format("Like: {0}", this.Likes.Count);
                this.likeBtn.Attributes.Add("disabled", "");
                this.likesPanel.Update();
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

        protected void delete_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.DeletePost(this.User.Identity.GetUserId());
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