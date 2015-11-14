using System;
namespace BlogSystem.Web
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web;

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
                this.presenter.Initialize(int.Parse(this.Request.QueryString["id"]));
            }
            catch (Exception)
            {
                Response.Redirect("Posts.aspx");   
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

        public CategoryView Category
        {
            set
            {
                this.category.Text = value.Name;
            }
        }

        public AuthorView Author
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

        public List<CommentView> Comments
        {
            set
            {
                this.comments.DataSource = value;
                this.DataBind();
            }
        }

        public List<TagView> Tags
        {
            set
            {
                this.tags.DataSource = value;
                this.DataBind();
            }
        }

        protected void addCommentBtn_OnClick(object sender, EventArgs e)
        {
            this.presenter.AddComment(this.commentAuthor.Text, this.addComment.Text, this.Id);
        }
    }
}