using Microsoft.AspNetCore.Identity;

namespace Sentinel.Api.Models;

public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; } = true;
    public DateTime? AccessGrantedUntil { get; set; }
}
