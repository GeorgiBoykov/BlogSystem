namespace BlogSystem.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Models;

    using Moq;

    public class MocksContainer
    {
       
        private static List<Category> fakeCategories = new List<Category>
                                                    {
                                                        new Category { Id = 1, Name = "TestCategory1" },
                                                        new Category { Id = 2, Name = "TestCategory2" }
                                                    };

        private static List<Tag> fakeTags = new List<Tag>
                                         {
                                             new Tag { Id = 1, Name = "TestTag1", Slug = "TestTag1" },
                                             new Tag { Id = 2, Name = "TestTag1", Slug = "TestTag2" }
                                         };


        private static List<Post> fakePosts = new List<Post>
                                                  {
                                                      new Post
                                                          {
                                                              Id = 1,
                                                              Author =
                                                                  new User
                                                                      {
                                                                          Id = "aaa",
                                                                          UserName = "Gosho"
                                                                      },
                                                              Category = fakeCategories.FirstOrDefault(),
                                                              Content =
                                                                  @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras non neque bibendum, facilisis leo quis, aliquam nulla. Donec lobortis sodales volutpat. Integer at ante id magna pretium eleifend et quis mauris. Mauris imperdiet viverra lacus vel tincidunt. Curabitur in vehicula lectus. Donec sit amet dolor et orci lacinia tempus. Praesent ligula metus, interdum eget purus in, vehicula hendrerit tellus. In hac habitasse platea dictumst. Aenean quis ullamcorper mauris. Maecenas volutpat sapien id nisl convallis suscipit. Suspendisse venenatis sem a lacus imperdiet convallis vitae vitae felis. In vestibulum sapien sed tincidunt gravida. Phasellus mollis sodales metus a efficitur. Sed ut ipsum ac nunc vestibulum tristique quis euismod est. Suspendisse potenti.
Fusce leo tortor, efficitur at hendrerit nec, gravida id elit. Vivamus placerat lorem non nunc egestas, eu placerat nisi cursus. Aliquam erat volutpat. Morbi vestibulum enim ut ante imperdiet egestas. Mauris posuere placerat facilisis. Donec porta porttitor semper. Praesent sed accumsan metus. Pellentesque non malesuada neque, nec semper enim.
Vestibulum eleifend, magna et euismod fermentum, urna neque tristique orci, sit amet efficitur quam quam ut libero. Proin elit turpis, efficitur eget imperdiet ac, dictum sit amet orci. Vestibulum vulputate quam nisl, vel interdum arcu lobortis eget. Vestibulum sodales iaculis erat id malesuada. Nulla consectetur suscipit lorem at mattis. Curabitur lobortis imperdiet nisl. Praesent tincidunt pulvinar velit, in sagittis nulla semper in. Nullam non lacus odio. Morbi congue facilisis suscipit. Vestibulum in ullamcorper neque, sed sollicitudin lacus. Sed nec erat vitae tortor elementum bibendum ut eu eros. Nulla facilisi.",
                                                              Title = "Test Post 1",
                                                              Slug = "Test-Post-1",
                                                              DateCreated = DateTime.Now,
                                                              Tags = fakeTags
                                                          },
                                                      new Post
                                                          {
                                                              Id = 2,
                                                              Author =
                                                                  new User
                                                                      {
                                                                          Id = "bbb",
                                                                          UserName = "Pesho"
                                                                      },
                                                              Category = fakeCategories.FirstOrDefault(),
                                                              Content =
                                                                  @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras non neque bibendum, facilisis leo quis, aliquam nulla. Donec lobortis sodales volutpat. Integer at ante id magna pretium eleifend et quis mauris. Mauris imperdiet viverra lacus vel tincidunt. Curabitur in vehicula lectus. Donec sit amet dolor et orci lacinia tempus. Praesent ligula metus, interdum eget purus in, vehicula hendrerit tellus. In hac habitasse platea dictumst. Aenean quis ullamcorper mauris. Maecenas volutpat sapien id nisl convallis suscipit. Suspendisse venenatis sem a lacus imperdiet convallis vitae vitae felis. In vestibulum sapien sed tincidunt gravida. Phasellus mollis sodales metus a efficitur. Sed ut ipsum ac nunc vestibulum tristique quis euismod est. Suspendisse potenti.
Fusce leo tortor, efficitur at hendrerit nec, gravida id elit.",
                                                              Title = "Test Post 2",
                                                              Slug = "Test-Post-2",
                                                              DateCreated = DateTime.Now,
                                                              Tags = fakeTags
                                                          }
                                                  };

        private static List<User> fakeUsers = new List<User>
                                                  {
                                                      new User
                                                          {
                                                              Id = "aaa",
                                                              UserName = "Gosho",
                                                              Posts = fakePosts
                                                          },
                                                      new User
                                                          {
                                                              Id = "bbb",
                                                              UserName = "Pesho",
                                                              Posts = new List<Post>()
                                                          }
                                                  };

        private static List<Comment> fakeComments = new List<Comment>
                                                        {
                                                            new Comment
                                                                {
                                                                    Author = "Gosho",
                                                                    Post = fakePosts.FirstOrDefault(),
                                                                    Content = "Test Comment 1",
                                                                    DateCreated = DateTime.Now
                                                                },
                                                            new Comment
                                                                {
                                                                    Author = "Pesho",
                                                                    Post = fakePosts.FirstOrDefault(),
                                                                    Content = "Test Comment 2",
                                                                    DateCreated = DateTime.Now
                                                                }
                                                        };


        public Mock<IBlogSystemData> DataMock;
        public Mock<IRepository<User>> UsersRepoMock;
        public Mock<IRepository<Category>> CategoriesRepoMock;
        public Mock<IRepository<Tag>> TagsRepoMock;
        public Mock<IRepository<Comment>> CommentsRepoMock;
        public Mock<IRepository<Post>> PostsRepoMock;

        public void SetupMocks()
        {
            this.UsersRepoMock = new Mock<IRepository<User>>();
            this.CategoriesRepoMock = new Mock<IRepository<Category>>();
            this.TagsRepoMock = new Mock<IRepository<Tag>>();
            this.CommentsRepoMock = new Mock<IRepository<Comment>>();
            this.PostsRepoMock = new Mock<IRepository<Post>>();

            this.UsersRepoMock.Setup(u => u.All()).Returns(fakeUsers.AsQueryable());
            this.CategoriesRepoMock.Setup(c => c.All()).Returns(fakeCategories.AsQueryable());
            this.TagsRepoMock.Setup(t => t.All()).Returns(fakeTags.AsQueryable());
            this.CommentsRepoMock.Setup(c => c.All()).Returns(fakeComments.AsQueryable());
            this.PostsRepoMock.Setup(p => p.All()).Returns(fakePosts.AsQueryable());

            this.DataMock = new Mock<IBlogSystemData>();
            this.DataMock.Setup(d => d.Users).Returns(this.UsersRepoMock.Object);
            this.DataMock.Setup(d => d.Categories).Returns(this.CategoriesRepoMock.Object);
            this.DataMock.Setup(d => d.Tags).Returns(this.TagsRepoMock.Object);
            this.DataMock.Setup(d => d.Posts).Returns(this.PostsRepoMock.Object);
            this.DataMock.Setup(d => d.Comments).Returns(this.CommentsRepoMock.Object);
        }
    }
}
