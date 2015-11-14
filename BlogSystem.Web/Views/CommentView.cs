using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Views
{
    using BlogSystem.Web.Views.Interfaces;

    public class CommentView : ICommentView
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }
    }
}