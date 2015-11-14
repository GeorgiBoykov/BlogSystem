namespace BlogSystem.Web.Models.ViewModels
{
    using BlogSystem.Web.Views;

    public class AuthorViewModel : IAuthorView
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}