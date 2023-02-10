using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.IdentityServer.Data;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions options) : base(options) { }
}
