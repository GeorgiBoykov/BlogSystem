using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Presenters
{
    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Views;

    public class TagPresenter : BasePresenter
    {
        private readonly ITagView view;

        public TagPresenter(ITagView view)
        {
            this.view = view;
        }

        public TagPresenter(ITagView view, IBlogSystemData data)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(string slug)
        {
            var tag = this.Data.Tags.All().FirstOrDefault(t => t.Slug == slug);

            if (tag == null)
            {
                throw new ArgumentException(string.Format("Tag with name {0} not found", slug));
            }

            var posts =
                tag.Posts
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

            this.view.Id = tag.Id;
            this.view.Name = tag.Name;
            this.view.Posts = posts;
        }
    }
}