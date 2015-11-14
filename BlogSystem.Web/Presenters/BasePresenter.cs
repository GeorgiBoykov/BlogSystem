namespace BlogSystem.Web.Presenters
{
    using BlogSystem.Data;
    using BlogSystem.Data.Interfaces;
    using BlogSystem.Data.UnitOfWork;

    public abstract class BasePresenter
    {
        protected BasePresenter()
            : this(new BlogSystemData(new BlogSystemDbContext()))
        {
        }

        private BasePresenter(IBlogSystemData data)
        {
            this.Data = data;
        }

        protected IBlogSystemData Data { get; set; }
    }
}