using AuthAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthDbContext: DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        // DbSet for UserInfo entity
        public DbSet<UserInfo> UsersData { get; set; } = null!;
    }
}
