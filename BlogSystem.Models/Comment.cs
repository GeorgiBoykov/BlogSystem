namespace BlogSystem.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public DateTime DateCreated { get; set; }
    }
}