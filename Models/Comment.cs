using Microsoft.AspNetCore.Identity;
using ForumApp.Models;
namespace ForumApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        
        // koppling till ForumThread
        public int ForumThreadId { get; set; }
        public ForumThread? ForumThread { get; set; }
        // koppling till User
        public string UserId { get; set; }= string.Empty;
         
         // svara på  kommentar
         public int? parentCommentId { get; set; }
         public Comment? ParentComment { get; set; } 
         public ICollection<Comment>? Replies { get; set; }
          // röstning
         public int VoteCount { get; set; } = 0;
    }
}