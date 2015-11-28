namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface IHomeView
    {
        List<PostViewModel> PostsFeed { set; }

        List<CommentViewModel> LatestComments { set; }

        List<TagViewModel> FamousTags { set; }
    }
}