namespace BlogSystem.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BlogSystem.Web.Models.ViewModels;
    using BlogSystem.Web.Presenters;
    using BlogSystem.Web.Views;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PresentersTests
    {
        private MocksContainer mocksContainer;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mocksContainer = new MocksContainer();
            this.mocksContainer.SetupMocks();
        }

        [TestMethod]
        public void BlogPresenterInitiliazingShouldReturnTwoPosts()
        {
            // Arrange
            List<PostViewModel> posts = new List<PostViewModel>();
            var blogViewMock = new Mock<IBlogView>();
            blogViewMock.SetupSet(t => t.Posts = It.IsAny<List<PostViewModel>>())
                .Callback<List<PostViewModel>>((list) => posts = list);
            var fakeBlogPresenter = new BlogPresenter(
                blogViewMock.Object,
                this.mocksContainer.DataMock.Object);
            
            // Act
            fakeBlogPresenter.Initialize("Gosho");
            
            // Assert
            Assert.AreEqual(2, posts.Count);
        }

        [TestMethod]
        public void PostPresenterInitilizingShouldReturnPostAttributes()
        {
            // Arrange
            string postTitle = null;
            CategoryViewModel postCategory = null;

            var postViewMock = new Mock<IPostView>();
            postViewMock.SetupSet(p => p.PostTitle = It.IsAny<string>()).Callback<string>((title) => postTitle = title);
            postViewMock.SetupSet(p => p.Category = It.IsAny<CategoryViewModel>()).Callback<CategoryViewModel>((category) => postCategory = category);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.Initialize("Gosho", "Test-Post-1");

            // Assert
            Assert.IsNotNull(postTitle);
            Assert.IsNotNull(postCategory);
        }

        [TestMethod]
        public void TagPresenterInitiliazingShouldReturnTwoPosts()
        {
            // Arrange
            List<PostViewModel> posts = new List<PostViewModel>();
            var tagViewMock = new Mock<ITagView>();
            tagViewMock.SetupSet(t => t.Posts = It.IsAny<List<PostViewModel>>())
                .Callback<List<PostViewModel>>((list) => posts = list);

            var fakeTagPresenter = new TagPresenter(tagViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeTagPresenter.Initialize("TestTag1");
            
            // Assert
            Assert.AreEqual(2, posts.Count);
        }

        [TestMethod]
        public void CategoryPresenterInitiliazingShouldReturnTwoPosts()
        {
            // Arrange
            List<PostViewModel> posts = new List<PostViewModel>();
            var categoryViewMock = new Mock<ICategoryView>();
            categoryViewMock.SetupSet(c => c.Posts = It.IsAny<List<PostViewModel>>())
                .Callback<List<PostViewModel>>((list) => posts = list);

            var fakeCategoryPresenter = new CategoryPresenter(
                this.mocksContainer.DataMock.Object,
                categoryViewMock.Object);

            // Act
            fakeCategoryPresenter.Initialize("TestCategory1");

            // Assert
            Assert.AreEqual(2, posts.Count);
        }

        [TestMethod]
        public void AddingNewPostWithValidDataShouldAddItToTheRepo()
        {
            // Arrange
            string title = "TestTitle";
            string content = "TestContent";
            string category = "TestCategory1";
            string tags = "tag1";
            string loggedUserId = "aaa";

            var newPostViewMock = new Mock<INewPostView>();
            newPostViewMock.Setup(v => v.PostTitle).Returns(title);
            newPostViewMock.Setup(v => v.Content).Returns(content);
            newPostViewMock.Setup(v => v.Category).Returns(category);
            newPostViewMock.Setup(v => v.Tags).Returns(tags);
            newPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeNewPostPresenter = new NewPostPresenter(newPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeNewPostPresenter.AddPost();

            // Assert
            Assert.AreEqual(3, this.mocksContainer.PostsRepoMock.Object.All().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingNewPostWhenUserIsNotLoggedShouldThrowException()
        {
            // Arrange
            string title = "TestTitle";
            string content = "TestContent";
            string category = "TestCategory1";
            string tags = "tag1";
            string loggedUserId = null;

            var newPostViewMock = new Mock<INewPostView>();
            newPostViewMock.Setup(v => v.PostTitle).Returns(title);
            newPostViewMock.Setup(v => v.Content).Returns(content);
            newPostViewMock.Setup(v => v.Category).Returns(category);
            newPostViewMock.Setup(v => v.Tags).Returns(tags);
            newPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeNewPostPresenter = new NewPostPresenter(newPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeNewPostPresenter.AddPost();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingNewPostWithInvalidDataShouldThrowException()
        {
            // Arrange
            string title = "";
            string content = "";
            string category = "";
            string tags = "tag1";
            string loggedUserId = "aaa";

            var newPostViewMock = new Mock<INewPostView>();
            newPostViewMock.Setup(v => v.PostTitle).Returns(title);
            newPostViewMock.Setup(v => v.Content).Returns(content);
            newPostViewMock.Setup(v => v.Category).Returns(category);
            newPostViewMock.Setup(v => v.Tags).Returns(tags);
            newPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeNewPostPresenter = new NewPostPresenter(newPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeNewPostPresenter.AddPost();
        }

        [TestMethod]
        public void AddNewCommentWithValidDataShouldAddItToTheRepo()
        {
            // Arrange
            string author = "testAuthor";
            string content = "testContent";
            int postId = 1;
            List<CommentViewModel> comments = new List<CommentViewModel>();

            var postViewMock = new Mock<IPostView>();
            postViewMock.Setup(v => v.Id).Returns(postId);
            postViewMock.Setup(v => v.Comments).Returns(comments);
            postViewMock.SetupSet(v => v.Comments = It.IsAny<List<CommentViewModel>>())
                .Callback<List<CommentViewModel>>((list) => comments = list);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.AddComment(author, content);

            // Assert
            Assert.AreEqual(3, this.mocksContainer.CommentsRepoMock.Object.All().Count());
            Assert.AreEqual(1, comments.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCommentWithNotValidDataShouldThrowExeption()
        {
            // Arrange
            string author = "";
            string content = "";
            int postId = 1;
            List<CommentViewModel> comments = new List<CommentViewModel>();

            var postViewMock = new Mock<IPostView>();
            postViewMock.Setup(v => v.Id).Returns(postId);
            postViewMock.Setup(v => v.Comments).Returns(comments);
            postViewMock.SetupSet(v => v.Comments = It.IsAny<List<CommentViewModel>>())
                .Callback<List<CommentViewModel>>((list) => comments = list);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.AddComment(author, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LikingPostByTheAuthorShouldThrowExeption()
        {
            // Arrange
            string loggedUserId = "aaa";
            string ipAddress = "";
            int postId = 1;
            var postAuthor = new AuthorViewModel { Id = loggedUserId };

            var postViewMock = new Mock<IPostView>();
            postViewMock.Setup(v => v.Id).Returns(postId);
            postViewMock.Setup(v => v.Author).Returns(postAuthor);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.LikePost(loggedUserId, ipAddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LikingAlredyLikedPostShouldThrowExeption()
        {
            // Arrange
            string loggedUserId = "aaa";
            string ipAddress = "";
            int postId = 1;
            var postAuthor = new AuthorViewModel { Id = "bbb" };

            var postViewMock = new Mock<IPostView>();
            postViewMock.Setup(v => v.Id).Returns(postId);
            postViewMock.Setup(v => v.Author).Returns(postAuthor);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.LikePost(loggedUserId, ipAddress);
        }

        [TestMethod]
        public void LikingPostShouldAddNewLikeInTheRepo()
        {
            // Arrange
            string loggedUserId = "bbb";
            string ipAddress = "fakeIp";
            int postId = 1;
            var postAuthor = new AuthorViewModel { Id = "aaa" };
            var likes = new List<LikeViewModel>();

            var postViewMock = new Mock<IPostView>();
            postViewMock.Setup(v => v.Id).Returns(postId);
            postViewMock.Setup(v => v.Author).Returns(postAuthor);
            postViewMock.Setup(v => v.Likes).Returns(likes);
            postViewMock.SetupSet(v => v.Likes = It.IsAny<List<LikeViewModel>>())
                .Callback<List<LikeViewModel>>((list) => likes = list);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.LikePost(loggedUserId, ipAddress);

            // Assert
            Assert.AreEqual(3, this.mocksContainer.LikesRepoMock.Object.All().Count());
        }

        [TestMethod]
        public void EditingExistingPostWithValidDataShouldChangePostAttributes()
        {
            // Arrange
            int postId = 1;
            string title = "testMoq";
            string content = "testMoq";
            string loggedUserId = "aaa";

            var editPostViewMock = new Mock<IEditPostView>();
            editPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeEditPostPresenter = new EditPostPresenter(editPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeEditPostPresenter.EditPost(postId,loggedUserId, title, content);

            // Assert
            Assert.AreEqual(title, this.mocksContainer.PostsRepoMock.Object.Find(postId).Title);
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditingExistingPostWithInvalidDataShouldThrowException()
        {
            // Arrange
            int postId = 1;
            string title = "";
            string content = "";
            string loggedUserId = "aaa";

            var editPostViewMock = new Mock<IEditPostView>();
            editPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeEditPostPresenter = new EditPostPresenter(editPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeEditPostPresenter.EditPost(postId,loggedUserId, title, content);
        }
    
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditingExistingPostByOtherThanTheAuthorShouldThrowException()
        {
            // Arrange
            int postId = 1;
            string title = "test";
            string content = "test";
            string loggedUserId = "bbb";

            var editPostViewMock = new Mock<IEditPostView>();
            editPostViewMock.Setup(v => v.AuthorId).Returns(loggedUserId);

            var fakeEditPostPresenter = new EditPostPresenter(editPostViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeEditPostPresenter.EditPost(postId,loggedUserId, title, content);
        }
    }
}
