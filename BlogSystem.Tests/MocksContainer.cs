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
                                                        new Category { Id = 2, Name = "TestCategory2"}
                                                    };

        private static List<Tag> fakeTags = new List<Tag>
                                         {
                                             new Tag { Id = 1, Name = "TestTag1", Slug = "TestTag1" },
                                             new Tag { Id = 2, Name = "TestTag1", Slug = "TestTag2" }
                                         };

        private static List<Like> fakeLikes = new List<Like>
                                                  {
                                                      new Like { Id = 1, IpAddress = "12.12.12.12", UserId = "aaa", PostId = 1},
                                                      new Like { Id = 2, IpAddress = "13.12.12.12" }
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
                                                              Content = "TestContent1",
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
                                                              Content = "TestContent2",
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
        public Mock<IRepository<Like>> LikesRepoMock;

        public void SetupMocks()
        {
            fakeCategories.FirstOrDefault(c => c.Name == "TestCategory1").Posts = fakePosts;
            fakeTags.FirstOrDefault(t => t.Name == "TestTag1").Posts = fakePosts;

            this.InitializeRepositories();
            this.SetupGetAllEntities();
            this.SetupAddEntity();
            this.SetupFindEntity();
            this.SetupGetData();
        }

        private void SetupGetData()
        {
            this.DataMock.Setup(d => d.Users).Returns(this.UsersRepoMock.Object);
            this.DataMock.Setup(d => d.Categories).Returns(this.CategoriesRepoMock.Object);
            this.DataMock.Setup(d => d.Tags).Returns(this.TagsRepoMock.Object);
            this.DataMock.Setup(d => d.Posts).Returns(this.PostsRepoMock.Object);
            this.DataMock.Setup(d => d.Comments).Returns(this.CommentsRepoMock.Object);
            this.DataMock.Setup(d => d.Likes).Returns(this.LikesRepoMock.Object);
        }

        private void SetupFindEntity()
        {
            this.PostsRepoMock.Setup(p => p.Find(It.IsAny<int>()))
                .Returns((int id) => fakePosts.FirstOrDefault(p => p.Id == id));
        }

        private void SetupAddEntity()
        {
            this.PostsRepoMock.Setup(p => p.Add(It.IsAny<Post>())).Callback((Post post) => fakePosts.Add(post));
            this.CommentsRepoMock.Setup(c => c.Add(It.IsAny<Comment>())).Callback((Comment comment) => fakeComments.Add(comment));
            this.TagsRepoMock.Setup(t => t.Add(It.IsAny<Tag>())).Callback((Tag tag) => fakeTags.Add(tag));
            this.LikesRepoMock.Setup(l => l.Add(It.IsAny<Like>())).Callback((Like like) => fakeLikes.Add(like));
        }

        private void SetupGetAllEntities()
        {
            this.UsersRepoMock.Setup(u => u.All()).Returns(fakeUsers.AsQueryable());
            this.CategoriesRepoMock.Setup(c => c.All()).Returns(fakeCategories.AsQueryable());
            this.TagsRepoMock.Setup(t => t.All()).Returns(fakeTags.AsQueryable());
            this.CommentsRepoMock.Setup(c => c.All()).Returns(fakeComments.AsQueryable());
            this.PostsRepoMock.Setup(p => p.All()).Returns(fakePosts.AsQueryable());
            this.LikesRepoMock.Setup(l => l.All()).Returns(fakeLikes.AsQueryable());
        }

        private void InitializeRepositories()
        {
            this.UsersRepoMock = new Mock<IRepository<User>>();
            this.CategoriesRepoMock = new Mock<IRepository<Category>>();
            this.TagsRepoMock = new Mock<IRepository<Tag>>();
            this.CommentsRepoMock = new Mock<IRepository<Comment>>();
            this.PostsRepoMock = new Mock<IRepository<Post>>();
            this.LikesRepoMock = new Mock<IRepository<Like>>();
            this.DataMock = new Mock<IBlogSystemData>();
        }
    }
}
