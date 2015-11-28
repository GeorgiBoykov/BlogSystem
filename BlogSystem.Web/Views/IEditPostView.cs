namespace BlogSystem.Web.Views
{
    public interface IEditPostView
    {
        int PostId { set; }

        string PostTitle { set; }

        string Content { set; }

        string Category { set; }

        string AuthorId { set; }

        string Tags { set; }
    }
}