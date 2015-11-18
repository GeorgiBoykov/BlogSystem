using System;
namespace BlogSystem.Web
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    public partial class Post : System.Web.UI.Page, IPostView
    {
        private readonly PostPresenter presenter;

        protected Post()
        {
            this.presenter = new PostPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var routeValues = this.RouteData.Values;
                this.presenter
                    .Initialize(routeValues["username"].ToString(), routeValues["slug"].ToString());
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new {ErrorMessage = ex.Message});
            }
        }

        public int Id { get; set; }

        public string PostTitle
        {
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

        public CategoryViewModel Category
        {
            set
            {
                this.category.Text = value.Name;
            }
        }

        public AuthorViewModel Author
        {
            set
            {
                this.author.Text = value.UserName;
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

        protected void addCommentBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.AddComment(this.addCommentAuthor.Text, this.addCommentContent.Text);
                this.DataBind();
                this.addCommentAuthor.Text = string.Empty;
                this.addCommentContent.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }
    }
}