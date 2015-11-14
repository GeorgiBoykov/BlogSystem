namespace BlogSystem.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using BlogSystem.Web.Views;

    public class PostView : IPostView
    {
        public int Id { get; set; }

        public string PostTitle { get; set; }

        public string Content { get; set; }

        public CategoryView Category { get; set; }

        public AuthorView Author { get; set; }

        public List<CommentView> Comments { get; set; }

        public List<TagView> Tags { get; set; }

        public DateTime DateCreated { get; set; }
    }
}