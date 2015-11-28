namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Utilities;
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

            var postsPreviews =
                user.Posts.OrderByDescending(p => p.DateCreated)
                    .Select(
                        p =>
                        new PostViewModel
                            {
                                Id = p.Id,
                                PostTitle = p.Title,
                                Slug = p.Slug,
                                Author =
                                    new AuthorViewModel { Id = p.AuthorId, UserName = p.Author.UserName },
                                Category =
                                    new CategoryViewModel { Id = p.CategoryId, Name = p.Category.Name },
                                Content =
                                    p.Content.Length > 300
                                        ? WebExtensions.TruncateHtml(p.Content, 300)
                                        : p.Content,
                                DateCreated = p.DateCreated
                            })
                    .ToList();

            this.view.Owner = new UserViewModel
                                  {
                                      Id = user.Id,
                                      Username = username,
                                      Followers =
                                          user.Followers.Select(
                                              u => new UserViewModel { Id = u.Id, Username = u.UserName })
                                          .Take(5)
                                          .ToList(),
                                      Following =
                                          user.Following.Select(
                                              u => new UserViewModel { Id = u.Id, Username = u.UserName })
                                          .Take(5)
                                          .ToList()
                                  };

            this.view.Posts = postsPreviews;
        }

        public void Follow(string loggedUserId)
        {
            if (loggedUserId == this.view.Owner.Id)
            {
                throw new ArgumentException("Users cannot follow themselves.");
            }

            var loggedUser = this.Data.Users.Find(loggedUserId);
            var userToFollow = this.Data.Users.Find(this.view.Owner.Id);

            loggedUser.Following.Add(userToFollow);

            this.Data.SaveChanges();
        }
    }
}