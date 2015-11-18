namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Models;
    using BlogSystem.Web.Views;

    using Microsoft.Ajax.Utilities;

    public class NewPostPresenter : BasePresenter
    {
        private readonly INewPostView view;

        public NewPostPresenter(INewPostView view)
        {
            this.view = view;
        }

        public NewPostPresenter(INewPostView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }

        public void Initialize()
        {
            var categories = this.Data.Categories.All().Select(
                c => c.Name).ToList();

            this.view.CategoriesList = categories;
        }

        public void AddPost()
        {
            if (this.view.PostTitle.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Title cannot be null or whitespace");
            }

            if (this.view.Content.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Content cannot be null or whitespace");
            }

            var category = this.Data.Categories.All().FirstOrDefault(c => c.Name == this.view.Category);

            if (category == null)
            {
                throw new ArgumentException("Not existing Category");
            }

            List<string> tagsNames = this.view.Tags
                .Split(',')
                .Select(p => p.Trim())
                .ToList();

            List<Tag> tags = new List<Tag>();

            if (tagsNames.Any(t => t != String.Empty))
            {
                tags = this.Data.Tags.All().Where(t => tagsNames.Contains(t.Name)).ToList();

                foreach (var tagName in tagsNames)
                {
                    if (!tags.Any(t => t.Name == tagName))
                    {
                        var tag = new Tag { Name = tagName };
                        this.Data.Tags.Add(tag);
                    }
                }

                this.Data.SaveChanges();
                tags = this.Data.Tags.All().Where(t => tagsNames.Contains(t.Name)).ToList();
            }

            var post = new BlogSystem.Models.Post
                           {
                               Title = this.view.PostTitle,
                               Slug = this.ResolveSubjectForUrl(this.view.PostTitle),
                               Content = this.view.Content,
                               CategoryId = category.Id,
                               AuthorId = this.view.AuthorId,
                               DateCreated = DateTime.Now,
                               Tags = tags
                           };


            this.Data.Posts.Add(post);
            this.Data.SaveChanges();
        }

        private string ResolveSubjectForUrl(string subject)
        {
            return Regex.Replace(Regex.Replace(subject, "[^\\w]", "-"), "[-]{2,}", "-");
        }
    }
}