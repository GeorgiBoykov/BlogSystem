using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Web.Views
{
    using BlogSystem.Web.Models.ViewModels;

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
