namespace BlogSystem.Web.Presenters
{
    using System;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Web.Views;

    using Microsoft.Ajax.Utilities;

    public class EditPostPresenter : BasePresenter
    {
        private readonly IEditPostView view;

        public EditPostPresenter(IEditPostView view)
        {
            this.view = view;
        }

        public EditPostPresenter(IEditPostView view, IBlogSystemData data)
        {
            this.view = view;
            this.Data = data;
        }

        public void Initialize(int id)
        {
            var post = this.Data.Posts.Find(id);
            
            this.view.PostId = id;
            this.view.PostTitle = post.Title;
            this.view.AuthorId = post.AuthorId;
            this.view.Category = post.Category.Name;
            this.view.Content = post.Content;
            this.view.Tags = string.Join(",", post.Tags.Select(t => t.Name).ToList());
        }

        public void EditPost(int id, string userId, string title, string content)
        {
            var post = this.Data.Posts.Find(id);

            if (post.AuthorId != userId)
            {
                throw new ArgumentException("Only authors can edit their posts.");
            }

            if (title.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Title cannot be null or whitespace");
            }

            if (content.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Content cannot be null or whitespace");
            }

            post.Title = title;
            post.Content = content;

            this.Data.SaveChanges();
        }
    }
}