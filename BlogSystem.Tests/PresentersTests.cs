namespace BlogSystem.Tests
{
    using System.Collections.Generic;
    
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
            blogViewMock.SetupSet(t => t.Posts).Callback((list) => posts = list);
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
            postViewMock.SetupSet(p => p.PostTitle).Callback((title) => postTitle = title);
            postViewMock.SetupSet(p => p.Category).Callback((category) => postCategory = category);

            var fakePostPresenter = new PostPresenter(postViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakePostPresenter.Initialize("Gosho", "Test-Post-1");

            // Assert
            Assert.IsNotNull(postTitle);
            Assert.IsNotNull(postCategory);
        }

        [TestMethod]
        public void CategoryPresenterInitiliazingShouldReturnTwoPosts()
        {
            // Arrange
            List<PostViewModel> posts = new List<PostViewModel>();
            var categoryViewMock = new Mock<ICategoryView>();
            categoryViewMock.SetupSet(t => t.Posts).Callback((list) => posts = list);
            var fakeCategoryPresenter = new CategoryPresenter(
                this.mocksContainer.DataMock.Object,
                categoryViewMock.Object);

            // Act
            fakeCategoryPresenter.Initialize("TestCategory1");
            
            // Assert
            Assert.AreEqual(2, posts.Count);
        }

        [TestMethod]
        public void TagPresenterInitiliazingShouldReturnTwoPosts()
        {
            // Arrange
            List<PostViewModel> posts = new List<PostViewModel>();
            var tagViewMock = new Mock<ITagView>();
            tagViewMock.SetupSet(t => t.Posts).Callback((list) => posts = list);
            var fakeTagPresenter = new TagPresenter(tagViewMock.Object, this.mocksContainer.DataMock.Object);

            // Act
            fakeTagPresenter.Initialize("TestTag1");
            
            // Assert
            Assert.AreEqual(2, posts.Count);
        }
    }
}
