namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class HomePresenter : BasePresenter
    {
        private IHomeView view;

        public HomePresenter(IHomeView view)
        {
            this.view = view;
        }

        public HomePresenter(IBlogSystemData data, IHomeView view)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(string loggedUserId)
        {
            var loggedUser = this.Data.Users.Find(loggedUserId);

            if (loggedUser == null)
            {
                throw new ArgumentException("This page requires to be logged in");
            }

            var following = loggedUser.Following.Select(f => f.Id);

            var postsFeed =
                this.Data.Posts.All()
                    .Where(p => following.Contains(p.AuthorId))
                    .OrderByDescending(p => p.DateCreated)
                    .Select(p => new PostViewModel
                                     {
                                         Slug = p.Slug,
                                         PostTitle = p.Title.Length > 80 ? p.Title.Substring(0, 80) + "..." : p.Title,
                                         Author = new AuthorViewModel { UserName = p.Author.UserName },
                                         DateCreated = p.DateCreated
                    })
                    .Take(5)
                    .ToList();

            var latestComments =
                this.Data.Comments.All()
                    .Where(c => following.Contains(c.Post.AuthorId) || c.Post.AuthorId == loggedUserId)
                    .OrderByDescending(c => c.DateCreated)
                    .Select(
                        c =>
                        new CommentViewModel
                            {
                                Author = c.Author,
                                Content = c.Content.Length > 80 ? c.Content.Substring(0, 80) + "..." : c.Content,
                                DateCreated = c.DateCreated,
                                Post = new PostViewModel
                                           {
                                               Author = new AuthorViewModel { UserName = c.Post.Author.UserName },
                                               Slug = c.Post.Slug
                                           }
                            })
                            .Take(5)
                            .ToList();

            var famousTags =
                this.Data.Tags.All()
                    .OrderByDescending(t => t.Posts.Count)
                    .Select(t => new TagViewModel { Name = t.Name, Slug = t.Slug })
                    .Take(15)
                    .ToList();

            this.view.PostsFeed = postsFeed;
            this.view.LatestComments = latestComments;
            this.view.FamousTags = famousTags;
        }
    }
}