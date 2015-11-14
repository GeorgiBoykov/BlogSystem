namespace BlogSystem.Data.Interfaces
{
    using BlogSystem.Data.Repositories;
    using BlogSystem.Models;

    public interface IBlogSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Tag> Tags { get; }
        
        IRepository<Post> Posts { get; }
        
        IRepository<Comment> Comments { get; }   
        
        int SaveChanges();
    }
}