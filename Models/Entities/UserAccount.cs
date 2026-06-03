using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models.Entities
{
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserName")]
        [MaxLength(100)]
        public string? UserName { get; set; }

         [Column("Password")]
         [MaxLength(100)]
            public string? Password { get; set; }
            
         [Column("Role")]
         [MaxLength(100)]
            public string? Role { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        
        // koppling till ForumThread
        public ICollection<ForumThread>? ForumThreads { get; set; }
        // koppling till Comment
        public ICollection<Comment>? Comments { get; set; }
    }
}