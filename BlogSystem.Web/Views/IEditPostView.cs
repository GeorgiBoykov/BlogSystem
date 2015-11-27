namespace BlogSystem.Web.Views
{
    using System.Collections.Generic;

    public interface IEditPostView
    {
        int PostId { get; set; }

        string PostTitle { get; set; }

        string Content { get; set; }

        string Category { get; set; }

        string AuthorId { get; set; }

        string Tags { get; set; }
    }
}
