namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Models;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    using Microsoft.Ajax.Utilities;

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

        public void Initialize(string username, string slug)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new ArgumentException(string.Format("User with username {0} not found", username));
            }

            var post = user.Posts.FirstOrDefault(p => p.Slug == slug);

            if (post == null)
            {
                throw new ArgumentException(string.Format("Post with title {0} not found", slug));
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
            this.view.Tags = post.Tags.Select(t => new TagViewModel { Id = t.Id, Name = t.Name, Slug = t.Slug}).ToList();
            this.view.Likes =
                post.Likes.Select(
                    l =>
                    new LikeViewModel
                        {
                            Id = l.Id,
                            PostId = l.PostId,
                            UserId = l.UserId,
                            IpAddress = l.IpAddress
                    })
                    .ToList();
        }

        public CommentViewModel AddComment(string author, string content)
        {
            if (author.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Missing author name.");
            }

            if (content.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Missing content.");
            }

            var comment = new Comment
            {
                Author = author,
                Content = content,
                DateCreated = DateTime.Now,
                PostId = this.view.Id
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

        public void LikePost(string userId, string ipAddress)
        {
            if (this.view.Author.Id == userId)
            {
                throw new ArgumentException("Authors cannot like their own posts");
            }

            if (this.Data.Likes.All()
                .Any(l => 
                (l.PostId == this.view.Id && l.UserId == userId) ||
                (l.PostId == this.view.Id && l.IpAddress == ipAddress) ))
            {
                throw new ArgumentException("Post already liked.");
            }

            var like = new Like { PostId = this.view.Id, UserId = userId, IpAddress = ipAddress };

            this.Data.Likes.Add(like);
            this.Data.SaveChanges();

            var likeView = new LikeViewModel
                               {
                                   Id = like.Id,
                                   PostId = like.PostId,
                                   UserId = userId,
                                   IpAddress = ipAddress
                               };

            this.view.Likes.Add(likeView);
            this.Data.SaveChanges();
        }
    }
}