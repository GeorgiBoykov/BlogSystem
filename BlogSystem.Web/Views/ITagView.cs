namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface ITagView
    {
        int Id { get; set; } 

        string Name { get; set; }

        List<PostViewModel> Posts { set; }
    }
}