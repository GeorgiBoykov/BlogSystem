namespace BlogSystem.Web.Models.ViewModels
{
    using BlogSystem.Web.Views;

    public class CategoryViewModel : ICategoryView
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}