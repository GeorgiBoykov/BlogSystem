namespace BlogSystem.Web.Views
{
    using System;

    using BlogSystem.Web.Models.ViewModels;

    public interface ICommentView
    {
        int Id { get; set; }

        string Content { get; set; }

        string Author { get; set; }

        PostViewModel Post { get; set; }

        DateTime DateCreated { get; set; }
    }
}