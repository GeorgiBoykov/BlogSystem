namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class BlogPresenter : BasePresenter
    {
        private readonly IBlogView view;

        public BlogPresenter(IBlogView view)
        {
            this.view = view;
        }

        public BlogPresenter(IBlogView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }
        
        public void Initialize(string username)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new ArgumentException(string.Format("User with username {0} not found", username));
            }
            
            var postsPreviews= user.Posts
                .OrderByDescending(p => p.DateCreated)
                .Select(
                        p =>
                        new PostViewModel
                        {
                            Id = p.Id,
                            PostTitle = p.Title,
                            Slug = p.Slug,
                            Author = new AuthorViewModel { Id = p.AuthorId, UserName = p.Author.UserName },
                            Category = new CategoryViewModel { Id = p.CategoryId, Name = p.Category.Name },
                            Content = p.Content.Length > 200 ? p.Content.Substring(0,200) + "..." : p.Content,
                            DateCreated = p.DateCreated,
                            Tags = p.Tags.Select(t => new TagViewModel { Id = t.Id, Name = t.Name }).ToList()
                        })
                    .ToList();

            this.view.Posts = postsPreviews;
        }
    }
}