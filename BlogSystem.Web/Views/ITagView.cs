namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface ITagView
    {
        int Id { set; }

        string Name { set; }

        List<PostViewModel> Posts { get; set; }
    }
}