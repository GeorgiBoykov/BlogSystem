namespace BlogSystem.Web.Models.ViewModels
{
    using System.Collections.Generic;

    using BlogSystem.Web.Views;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}