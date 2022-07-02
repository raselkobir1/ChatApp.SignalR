using Microsoft.AspNetCore.Identity;

namespace ChatApp.SignalR.Models
{
    public class AppUser: IdentityUser
    {
        //one appUser has many message 1 to many relation
        public AppUser()
        {
            Messages = new HashSet<Message>();   
        }
        public virtual ICollection<Message> Messages { get; set; }  
    }
}
