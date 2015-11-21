namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface ICategoryView
    {
        int Id { get; set; }

        string Name { get; set; }

        List<PostViewModel> Posts { set; }
    }
}
