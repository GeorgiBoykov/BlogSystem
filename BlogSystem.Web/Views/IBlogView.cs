namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface IBlogView
    {
        UserViewModel Owner { get; set; }

        List<PostViewModel> Posts { get; set; }

        int PagesCount { get; set; }

        int CurrentPage { get; set; }
    }
}