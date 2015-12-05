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

        private const int DefaultPostsPerPage = 5;
        private const int DefaultPreviewsLenght = 300;
        private const int DefaultFollowersCount = 5;

        public BlogPresenter(IBlogView view)
        {
            this.view = view;
        }

        public BlogPresenter(IBlogView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }

        public void Initialize(string username, int page = 1)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new ArgumentException(string.Format("User with username {0} not found", username));
            }

            var postsPreviews =
                user.Posts.Where(p => p.IsDeleted == false)
                    .OrderByDescending(p => p.DateCreated)
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
                                    p.Content.Length > DefaultPreviewsLenght
                                        ? WebExtensions.TruncateHtml(p.Content, DefaultPreviewsLenght)
                                        : p.Content,
                                DateCreated = p.DateCreated
                            })
                    .Skip((page - 1) * DefaultPostsPerPage)
                    .Take(DefaultPostsPerPage)
                    .ToList();

            this.view.Owner = new UserViewModel
                                  {
                                      Id = user.Id,
                                      Username = username,
                                      Followers =
                                          user.Followers.Select(
                                              u => new UserViewModel { Id = u.Id, Username = u.UserName })
                                          .Take(DefaultFollowersCount)
                                          .ToList(),
                                      Following =
                                          user.Following.Select(
                                              u => new UserViewModel { Id = u.Id, Username = u.UserName })
                                          .Take(DefaultFollowersCount)
                                          .ToList()
                                  };

            this.view.Posts = postsPreviews;

            this.view.CurrentPage = page;

            this.view.PagesCount = (int)Math.Ceiling((double)user.Posts.Count(p => !p.IsDeleted) / DefaultPostsPerPage);
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