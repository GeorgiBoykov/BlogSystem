namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface ISearchView
    {
        List<PostViewModel> PostResults { get; set; }

        List<UserViewModel> UserResults { get; set; } 
    }
}
