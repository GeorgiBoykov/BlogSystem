namespace BlogSystem.Web.Views.Interfaces
{
    using System.Collections.Generic;

    public interface IPostsView
    {
        List<PostView> PostItems { set; }
    }
}
