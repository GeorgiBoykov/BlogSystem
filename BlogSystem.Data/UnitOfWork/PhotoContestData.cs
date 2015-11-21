namespace BlogSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using BlogSystem.Data.Interfaces;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Models;

    public class BlogSystemData : IBlogSystemData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        public BlogSystemData()
            : this(new BlogSystemDbContext())
        {
        }

        public BlogSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return this.GetRepository<Tag>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<Like> Likes
        {
            get
            {
                return this.GetRepository<Like>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericEfRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}