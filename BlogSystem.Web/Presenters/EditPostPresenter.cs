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

        public void Initialize(string slug)
        {
            var post = this.Data.Posts.All().FirstOrDefault(p => p.Slug == slug);

            if (post == null)
            {
                throw new ArgumentException(string.Format("Not existing post: {0}", slug));
            }

            this.view.PostId = post.Id;
            this.view.PostTitle = post.Title;
            this.view.AuthorId = post.AuthorId;
            this.view.Category = post.Category.Name;
            this.view.Content = post.Content;
            this.view.Tags = string.Join(",", post.Tags.Select(t => t.Name).ToList());
        }

        public void EditPost(int id, string userId, string title, string content)
        {
            var post = this.Data.Posts.Find(id);

            if (post.IsDeleted)
            {
                throw new ArgumentException(string.Format("Post with title {0} was deleted", post.Title));
            }

            if (post.Author.Id != userId)
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