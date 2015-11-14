namespace BlogSystem.Web.Models.ViewModels
{
    using BlogSystem.Web.Views;

    public class AuthorView : IAuthorView
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}