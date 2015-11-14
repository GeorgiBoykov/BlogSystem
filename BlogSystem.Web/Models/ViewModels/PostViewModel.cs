namespace BlogSystem.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using BlogSystem.Web.Views;

    public class PostViewModel : IPostView
    {
        public int Id { get; set; }

        public string PostTitle { get; set; }

        public string Content { get; set; }

        public CategoryViewModel Category { get; set; }

        public AuthorViewModel Author { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public DateTime DateCreated { get; set; }
    }
}