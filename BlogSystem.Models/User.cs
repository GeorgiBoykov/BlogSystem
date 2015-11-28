namespace BlogSystem.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public User()
        {
            this.Posts = new HashSet<Post>();
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
        }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}