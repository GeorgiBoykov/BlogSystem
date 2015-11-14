namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Models;
    using BlogSystem.Web.Models;
    using BlogSystem.Web.Views;
    using BlogSystem.Web.Views.Interfaces;

    public class PostPresenter : BasePresenter
    {
        private readonly IPostView view;

        public PostPresenter(IPostView view)
        {
            this.view = view;
        }

        public PostPresenter(IPostView view, IBlogSystemData data)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(int id)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            this.view.PostTitle = post.Title;
            this.view.Category = new CategoryView {Id = post.CategoryId, Name = post.Category.Name};
            this.view.Author = new AuthorView { Id = post.AuthorId, UserName = post.Author.UserName };
            this.view.Content = post.Content;
            this.view.DateCreated = post.DateCreated;
            this.view.Id = post.Id;
            this.view.Comments =
                post.Comments.Select(
                    c =>
                    new CommentView
                        {
                            Id = c.Id,
                            Author = c.Author,
                            Content = c.Content
                        }).ToList();
            this.view.Tags = post.Tags.Select(t => new TagView { Id = t.Id, Name = t.Name }).ToList();
        }

        public void AddComment(string author, string content, int postId)
        {
            var comment = new Comment
            {
                Author = author,
                Content = content,
                DateCreated = DateTime.Now,
                PostId = postId
            };

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();
        }
    }
}