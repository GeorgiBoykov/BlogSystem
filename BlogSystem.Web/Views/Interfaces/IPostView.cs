namespace BlogSystem.Web.Views.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IPostView
    {
        int Id { set; }

        string PostTitle { set; }

        string Content { set; }

        CategoryView Category { set; }

        AuthorView Author { set; }

        DateTime DateCreated { set; }

        List<CommentView> Comments { set; }

        List<TagView> Tags { set; }
    }
}
