namespace BlogSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using BlogSystem.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogSystemDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.SeedDefaultApplicationRoles(context);
            }

            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Categories.Any())
            {
                this.SeedCategories(context);
                context.SaveChanges();
            }
            
            if (!context.Tags.Any())
            {
                this.SeedTags(context);
                context.SaveChanges();
            }
            
            if (!context.Posts.Any())
            {
                this.SeedPosts(context);
                context.SaveChanges();
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
                context.SaveChanges();
            }

        }

        private void SeedComments(BlogSystemDbContext context)
        {

            var comment1 = new Comment
                               {
                                   Author = "Gosho",
                                   PostId = context.Posts.FirstOrDefault().Id,
                                   Content = "Test Comment 1",
                                   DateCreated = DateTime.Now
                               };
            var comment2 = new Comment
                               {
                                   Author = "Pesho",
                                   PostId = context.Posts.FirstOrDefault().Id,
                                   Content = "Test Comment 2",
                                   DateCreated = DateTime.Now
                               };

            context.Comments.AddOrUpdate(comment1, comment2);
        }

        private void SeedTags(BlogSystemDbContext context)
        {
            var tag1 = new Tag { Name = "TestTag1" };
            var tag2 = new Tag { Name = "TestTag1" };

            context.Tags.AddOrUpdate(tag1, tag2);
        }

        private void SeedCategories(BlogSystemDbContext context)
        {
            var category1 = new Category { Name = "TestCategory1" };
            var category2 = new Category { Name = "TestCategory2" };

            context.Categories.AddOrUpdate(category1, category2);
        }

        private void SeedPosts(BlogSystemDbContext context)
        {
            var post1 = new Post
                            {
                                AuthorId = context.Users.FirstOrDefault().Id,
                                CategoryId = context.Categories.FirstOrDefault().Id,
                                Content = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras non neque bibendum, facilisis leo quis, aliquam nulla. Donec lobortis sodales volutpat. Integer at ante id magna pretium eleifend et quis mauris. Mauris imperdiet viverra lacus vel tincidunt. Curabitur in vehicula lectus. Donec sit amet dolor et orci lacinia tempus. Praesent ligula metus, interdum eget purus in, vehicula hendrerit tellus. In hac habitasse platea dictumst. Aenean quis ullamcorper mauris. Maecenas volutpat sapien id nisl convallis suscipit. Suspendisse venenatis sem a lacus imperdiet convallis vitae vitae felis. In vestibulum sapien sed tincidunt gravida. Phasellus mollis sodales metus a efficitur. Sed ut ipsum ac nunc vestibulum tristique quis euismod est. Suspendisse potenti.
Fusce leo tortor, efficitur at hendrerit nec, gravida id elit. Vivamus placerat lorem non nunc egestas, eu placerat nisi cursus. Aliquam erat volutpat. Morbi vestibulum enim ut ante imperdiet egestas. Mauris posuere placerat facilisis. Donec porta porttitor semper. Praesent sed accumsan metus. Pellentesque non malesuada neque, nec semper enim.
Vestibulum eleifend, magna et euismod fermentum, urna neque tristique orci, sit amet efficitur quam quam ut libero. Proin elit turpis, efficitur eget imperdiet ac, dictum sit amet orci. Vestibulum vulputate quam nisl, vel interdum arcu lobortis eget. Vestibulum sodales iaculis erat id malesuada. Nulla consectetur suscipit lorem at mattis. Curabitur lobortis imperdiet nisl. Praesent tincidunt pulvinar velit, in sagittis nulla semper in. Nullam non lacus odio. Morbi congue facilisis suscipit. Vestibulum in ullamcorper neque, sed sollicitudin lacus. Sed nec erat vitae tortor elementum bibendum ut eu eros. Nulla facilisi.",
                                Title = "Test Post 1",
                                DateCreated = DateTime.Now,
                                Tags = new List<Tag>(context.Tags),
            };

            var post2 = new Post
                            {
                                AuthorId = context.Users.FirstOrDefault().Id,
                                CategoryId = context.Categories.FirstOrDefault().Id,
                Content = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras non neque bibendum, facilisis leo quis, aliquam nulla. Donec lobortis sodales volutpat. Integer at ante id magna pretium eleifend et quis mauris. Mauris imperdiet viverra lacus vel tincidunt. Curabitur in vehicula lectus. Donec sit amet dolor et orci lacinia tempus. Praesent ligula metus, interdum eget purus in, vehicula hendrerit tellus. In hac habitasse platea dictumst. Aenean quis ullamcorper mauris. Maecenas volutpat sapien id nisl convallis suscipit. Suspendisse venenatis sem a lacus imperdiet convallis vitae vitae felis. In vestibulum sapien sed tincidunt gravida. Phasellus mollis sodales metus a efficitur. Sed ut ipsum ac nunc vestibulum tristique quis euismod est. Suspendisse potenti.
Fusce leo tortor, efficitur at hendrerit nec, gravida id elit.",
                Title = "Test Post 2",
                                DateCreated = DateTime.Now,
                                Tags = new List<Tag>(context.Tags),
                            };

            context.Posts.AddOrUpdate(post1, post2);
        }

        private void SeedDefaultApplicationRoles(BlogSystemDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            var adminRole = new IdentityRole { Name = "Admin" };

            manager.Create(adminRole);
        }

        private void SeedUsers(BlogSystemDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var userToInsert = new User { UserName = "admin", Email = "georgipetrov02@gmail.com" };
            userManager.Create(userToInsert, "Aa-1234");
            userManager.AddToRole(userToInsert.Id, "Admin");
        }
    }
}