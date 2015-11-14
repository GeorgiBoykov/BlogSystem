namespace BlogSystem.Web.Models.ViewModels
{
    using BlogSystem.Web.Views;

    public class CategoryView : ICategoryView
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}