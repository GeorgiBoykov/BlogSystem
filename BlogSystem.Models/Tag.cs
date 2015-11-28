namespace BlogSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}