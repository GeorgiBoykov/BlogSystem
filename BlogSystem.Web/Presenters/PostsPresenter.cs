namespace BlogSystem.Web.Presenters
{
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class PostsPresenter : BasePresenter
    {
        private readonly IPostsView view;

        public PostsPresenter(IPostsView view)
        {
            this.view = view;
        }

        public PostsPresenter(IPostsView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }
        
        public void Initialize()
        {
            var postsPreviews= this.Data.Posts.All()
                .Select(
                        p =>
                        new PostView
                        {
                            Id = p.Id,
                            PostTitle = p.Title,
                            Author = new AuthorView { Id = p.AuthorId, UserName = p.Author.UserName },
                            Category = new CategoryView { Id = p.CategoryId, Name = p.Category.Name },
                            Content = p.Content.Substring(0,200) + "...",
                            Tags = p.Tags.Select(t => new TagView { Id = t.Id, Name = t.Name }).ToList()
                        })
                    .ToList();

            this.view.PostItems = postsPreviews;
        }
    }
}