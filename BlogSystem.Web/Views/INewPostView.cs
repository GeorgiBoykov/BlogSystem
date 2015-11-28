namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    public interface INewPostView
    {
        string PostTitle { get; }

        string Content { get; }

        string Category { get; }

        List<string> CategoriesList { set; }

        string AuthorId { get; }

        string Tags { get; }
    }
}