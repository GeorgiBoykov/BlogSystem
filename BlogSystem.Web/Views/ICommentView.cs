namespace BlogSystem.Web.Views
{
    using System;

    public interface ICommentView
    {
        int Id { get; set; }

        string Content { get; set; }

        string Author { get; set; }

        DateTime DateCreated { get; set; }
    }
}
