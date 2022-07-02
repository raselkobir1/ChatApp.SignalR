using System.ComponentModel.DataAnnotations;

namespace ChatApp.SignalR.Models
{
    public class Message
    {
        public Message()
        {
            When = DateTime.UtcNow;
        }
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
