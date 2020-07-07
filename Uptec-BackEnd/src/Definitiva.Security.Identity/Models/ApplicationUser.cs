using Microsoft.AspNetCore.Identity;

namespace Definitiva.Security.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ApiKey { get; set; }
    }
}