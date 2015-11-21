using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Presenters
{
    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class CategoryPresenter : BasePresenter
    {
        private readonly ICategoryView view;

        public CategoryPresenter(ICategoryView view)
        {
            this.view = view;
        }

        public CategoryPresenter(IBlogSystemData data, ICategoryView view)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(string categoryName)
        {
            var category = this.Data.Categories.All().FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                throw new ArgumentException(string.Format("Category with name {0} not found", categoryName));
            }

            var posts =
                category.Posts
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
                        Content = p.Content.Length > 200 ? p.Content.Substring(0, 200) + "..." : p.Content,
                        DateCreated = p.DateCreated
                    }).ToList();

            this.view.Id = category.Id;
            this.view.Name = category.Name;
            this.view.Posts = posts;
        }
    }
}