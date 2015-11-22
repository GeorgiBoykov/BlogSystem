namespace BlogSystem.Web.Models.ViewModels
{
    using System;

    using BlogSystem.Web.Views;

    public class CommentViewModel : ICommentView
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public PostViewModel Post { get; set; }

        public DateTime DateCreated { get; set; }
    }
}