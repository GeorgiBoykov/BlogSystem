namespace BlogSystem.Web.Presenters
{
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class SearchPresenter : BasePresenter
    {
        private ISearchView view;

        public SearchPresenter(ISearchView view)
        {
            this.view = view;
        }

        public SearchPresenter(ISearchView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }

        public void Initialize(string searchTerm)
        {
            var postResults =
                this.Data.Posts.All()
                    .Where(p => p.Title.Contains(searchTerm))
                    .Select(
                        p =>
                        new PostViewModel
                            {
                                Id = p.Id,
                                PostTitle = p.Title,
                                Slug = p.Slug,
                                Author = new AuthorViewModel { UserName = p.Author.UserName }
                            })
                    .ToList();

            var userResults =
                this.Data.Users.All()
                    .Where(u => u.UserName.Contains(searchTerm))
                    .Select(u => new UserViewModel { Id = u.Id, Username = u.UserName })
                    .ToList();

            this.view.PostResults = postResults;
            this.view.UserResults = userResults;
        }
    }
}