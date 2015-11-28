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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Followers).WithMany(u => u.Following).Map(
                m =>
                    {
                        m.ToTable("UserFollowers");
                        m.MapLeftKey("UserId");
                        m.MapRightKey("FollowerId");
                    });

            base.OnModelCreating(modelBuilder);
        }

        public static BlogSystemDbContext Create()
        {
            return new BlogSystemDbContext();
        }
    }
}