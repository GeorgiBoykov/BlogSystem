using System;
namespace BlogSystem.Web
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.AspNet.Identity;

    public partial class Post : System.Web.UI.Page, IPostView
    {
        private readonly PostPresenter presenter;

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
                this.category.NavigateUrl = string.Format("../categories/show/{0}",value.Name);
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
            }
        }

        protected void addCommentBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.AddComment(this.addCommentAuthor.Text, this.addCommentContent.Text);
                this.DataBind();
                this.commentsPanel.Update();
                this.addCommentAuthor.Text = string.Empty;
                this.addCommentContent.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        protected void likeBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.presenter.LikePost(this.User.Identity.GetUserId() ?? this.GetUserIP());
                this.likeBtn.Text = string.Format("Like: {0}", this.Likes.Count);
                this.likesPanel.Update();
            }
            catch (Exception ex)
            {
                this.Response.RedirectToRoute("CustomErrorPage", new { ErrorMessage = ex.Message });
            }
        }

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0].Split(':')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"].Split(':')[0];
        }
    }
}