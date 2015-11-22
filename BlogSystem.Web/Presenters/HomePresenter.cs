namespace BlogSystem.Web.Presenters
{
    using System.Data.Entity;
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
            var following = loggedUser.Following.Select(f => f.Id);

            var latestPosts =
                this.Data.Posts.All()
                    .Where(p => following.Contains(p.AuthorId))
                    .OrderByDescending(p => p.DateCreated)
                    .Select(p => new PostViewModel
                                     {
                                         Slug = p.Slug,
                                         PostTitle = p.Title,
                                         Content = p.Content.Length > 300 ? p.Content.Substring(0, 300) + "..." : p.Content,
                                         Author = new AuthorViewModel { UserName = p.Author.UserName }
                    })
                    .ToList();

            var latestComments =
                this.Data.Comments.All()
                    .Where(c => c.Post.AuthorId == loggedUserId)
                    .OrderByDescending(c => c.DateCreated)
                    .Select(
                        c =>
                        new CommentViewModel
                            {
                                Author = c.Author,
                                Content = c.Content.Length > 100 ? c.Content.Substring(0, 100) + "..." : c.Content,
                                DateCreated = c.DateCreated,
                                Post = new PostViewModel
                                           {
                                               Author = new AuthorViewModel { UserName = c.Post.Author.UserName },
                                               Slug = c.Post.Slug
                                           }
                            })
                            .ToList();

            var famousTags =
                this.Data.Tags.All()
                    .OrderByDescending(t => t.Posts.Count)
                    .Select(t => new TagViewModel { Name = t.Name, Slug = t.Slug })
                    .Take(10)
                    .ToList();

            this.view.LatestPosts = latestPosts;
            this.view.LatestComments = latestComments;
            this.view.FamousTags = famousTags;
        }
    }
}