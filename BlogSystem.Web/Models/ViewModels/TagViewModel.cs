namespace BlogSystem.Web.Models.ViewModels
{
    using BlogSystem.Web.Views;

    public class TagViewModel : ITagView
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}