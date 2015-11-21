namespace BlogSystem.Data
{
    using System.Data.Entity;

    using BlogSystem.Data.Migrations;
    using BlogSystem.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class BlogSystemDbContext : IdentityDbContext<User>
    {
        public BlogSystemDbContext()
            : base("BlogSystemDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogSystemDbContext, Configuration>());
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; } 

        public static BlogSystemDbContext Create()
        {
            return new BlogSystemDbContext();
        }
    }
}