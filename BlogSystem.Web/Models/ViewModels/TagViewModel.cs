namespace BlogSystem.Web.Models.ViewModels
{
    using System.Collections.Generic;

    using BlogSystem.Web.Views;

    public class TagViewModel : ITagView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}