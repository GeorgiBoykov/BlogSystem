namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Utilities;
    using BlogSystem.Web.Views;

    public class CategoryPresenter : BasePresenter
    {
        private readonly ICategoryView view;

        private const int DefaultPostsPerPage = 5;
        private const int DefaultPreviewsLenght = 300;

        public CategoryPresenter(ICategoryView view)
        {
            this.view = view;
        }

        public CategoryPresenter(IBlogSystemData data, ICategoryView view)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(string categoryName, int page = 1)
        {
            var category = this.Data.Categories.All().FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                throw new ArgumentException(string.Format("Category with name {0} not found", categoryName));
            }

            var posts =
                category.Posts.OrderByDescending(p => p.DateCreated)
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

            this.view.Id = category.Id;
            this.view.Name = category.Name;
            this.view.Posts = posts;

            this.view.CurrentPage = page;
            this.view.PagesCount = (int)Math.Ceiling((double)category.Posts.Count / DefaultPostsPerPage);
        }
    }
}