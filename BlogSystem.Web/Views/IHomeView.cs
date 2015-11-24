namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    using BlogSystem.Web.Models.ViewModels;

    public interface IHomeView
    {
        List<PostViewModel> PostsFeed { get; set; }

        List<CommentViewModel> LatestComments { get; set; } 
        
        List<TagViewModel> FamousTags { get; set; }
    }
}
