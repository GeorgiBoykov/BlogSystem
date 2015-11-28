namespace BlogSystem.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public List<UserViewModel> Followers { get; set; }

        public List<UserViewModel> Following { get; set; }
    }
}