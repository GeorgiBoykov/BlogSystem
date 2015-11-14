using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Views
{
    using BlogSystem.Web.Views.Interfaces;

    public class TagView : ITagView
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}