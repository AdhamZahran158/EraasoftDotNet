using Microsoft.AspNetCore.Identity;

namespace Task13_v2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string? Address { get; set; }
    }
}
