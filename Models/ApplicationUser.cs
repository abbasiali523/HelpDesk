using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
