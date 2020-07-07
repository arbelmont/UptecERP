using Microsoft.AspNetCore.Identity;

namespace Definitiva.Security.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string ApiKey { get; set; }
    }
}