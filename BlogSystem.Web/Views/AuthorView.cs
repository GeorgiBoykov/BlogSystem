using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Views
{
    using BlogSystem.Web.Views.Interfaces;

    public class AuthorView : IAuthorView
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}