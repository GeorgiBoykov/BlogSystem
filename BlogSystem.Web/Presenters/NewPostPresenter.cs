﻿namespace BlogSystem.Web.Presenters
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
            var categories = this.Data.Categories.All().Select(c => c.Name).ToList();

            this.view.CategoriesList = categories;
        }

        public void AddPost()
        {
            Random rnd = new Random();

            if (this.view.AuthorId == null)
            {
                throw new ArgumentException("User have to be logged in to add new post");
            }

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

            List<string> tagsNames = this.view.Tags.Split(',').Select(p => p.Trim()).ToList();

            List<Tag> tags = new List<Tag>();

            if (tagsNames.Any(t => t != string.Empty))
            {
                tags = this.Data.Tags.All().Where(t => tagsNames.Contains(t.Name)).ToList();

                foreach (var tagName in tagsNames)
                {
                    if (!tags.Any(t => t.Name == tagName))
                    {
                        var tag = new Tag { Name = tagName, Slug = this.CreateSlug(tagName) };
                        this.Data.Tags.Add(tag);
                    }
                }

                this.Data.SaveChanges();
                tags = this.Data.Tags.All().Where(t => tagsNames.Contains(t.Name)).ToList();
            }

            var post = new Post
                           {
                               Title = this.view.PostTitle,
                               Slug =
                                   this.CreateSlug(
                                       string.Format("{0}-{1}", this.view.PostTitle, rnd.Next(10000,100000))),
                               Content = this.view.Content,
                               CategoryId = category.Id,
                               AuthorId = this.view.AuthorId,
                               DateCreated = DateTime.Now,
                               Tags = tags
                           };

            this.Data.Posts.Add(post);
            this.Data.SaveChanges();
        }

        private string CreateSlug(string subject)
        {
            return Regex.Replace(Regex.Replace(subject, "[^\\w]", "-"), "[-]{2,}", "-");
        }
    }
}