namespace BlogSystem.Web.Views
{
    using System;
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface IPostView
    {
        int Id { get; set; }

        string PostTitle { get; set; }

        string Content { set; }

        string Slug { set; }

        CategoryViewModel Category { get; set; }

        AuthorViewModel Author { get; set; }

        DateTime DateCreated { set; }

        List<CommentViewModel> Comments { get; set; }

        List<TagViewModel> Tags { set; }

        List<LikeViewModel> Likes { get; set; }
    }
}