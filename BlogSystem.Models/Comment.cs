namespace BlogSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        [MinLength(2)]
        public string Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public DateTime DateCreated { get; set; }
    }
}