using ForumApp.Models;
namespace ForumApp.Models
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        // koppling till Category
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }
        // koppling till User
        public string UserId { get; set; }= string.Empty;
        public ICollection<Comment>? Comments { get; set; }
    }
}