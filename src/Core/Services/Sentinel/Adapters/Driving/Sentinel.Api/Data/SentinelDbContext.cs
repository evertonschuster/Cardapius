using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sentinel.Api.Data;

public class SentinelDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public SentinelDbContext(DbContextOptions<SentinelDbContext> options) : base(options) { }
}

