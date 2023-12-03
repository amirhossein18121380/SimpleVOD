using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VOD.Models;

namespace VOD.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
