namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    public interface INewPostView
    {
        string PostTitle { set; get; }

        string Content { set; get; }

        string Category { set; get; }

        List<string> CategoriesList { set; }

        string AuthorId { get; }

        string Tags { get; }
    }
}