namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface IPostsView
    {
        List<PostView> PostItems { set; }
    }
}
