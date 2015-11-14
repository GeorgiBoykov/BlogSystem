using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Views
{
    using BlogSystem.Web.Views.Interfaces;

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