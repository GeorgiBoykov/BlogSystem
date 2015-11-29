namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Utilities;
    using BlogSystem.Web.Views;

    public class TagPresenter : BasePresenter
    {
        private readonly ITagView view;

        private const int DefaultPostsPerPage = 5;
        private const int DefaultPreviewsLenght = 300;

        public TagPresenter(ITagView view)
        {
            this.view = view;
        }

        public TagPresenter(ITagView view, IBlogSystemData data)
        {
            this.Data = data;
            this.view = view;
        }

        public void Initialize(string slug, int page = 1)
        {
            var tag = this.Data.Tags.All().FirstOrDefault(t => t.Slug.ToLower() == slug.ToLower());

            if (tag == null)
            {
                throw new ArgumentException(string.Format("Tag with name {0} not found", slug));
            }

            var posts =
                tag.Posts.OrderByDescending(p => p.DateCreated)
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

            this.view.Id = tag.Id;
            this.view.Name = tag.Name;
            this.view.Posts = posts;

            this.view.CurrentPage = page;
            this.view.PagesCount = (int)Math.Ceiling((double)tag.Posts.Count / DefaultPostsPerPage);
        }
    }
}