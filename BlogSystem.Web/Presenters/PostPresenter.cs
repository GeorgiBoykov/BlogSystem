namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Models;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

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
                throw new ArgumentException(string.Format("Post with id {0} not found", id));
            }

            this.view.PostTitle = post.Title;
            this.view.Category = new CategoryViewModel {Id = post.CategoryId, Name = post.Category.Name};
            this.view.Author = new AuthorViewModel { Id = post.AuthorId, UserName = post.Author.UserName };
            this.view.Content = post.Content;
            this.view.DateCreated = post.DateCreated;
            this.view.Id = post.Id;
            this.view.Comments =
                post.Comments
                .OrderByDescending(c => c.DateCreated)
                .Select(
                    c =>
                    new CommentViewModel
                        {
                            Id = c.Id,
                            Author = c.Author,
                            Content = c.Content,
                            DateCreated = c.DateCreated
                        }).ToList();
            this.view.Tags = post.Tags.Select(t => new TagViewModel { Id = t.Id, Name = t.Name }).ToList();
        }

        public CommentViewModel AddComment(string author, string content, int postId)
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

            var commentView = new CommentViewModel
            {
                Id = comment.Id,
                Content = comment.Content,
                Author = comment.Author,
                DateCreated = comment.DateCreated
            };

            this.view.Comments.Insert(0, commentView);

            return commentView;
        }
    }
}