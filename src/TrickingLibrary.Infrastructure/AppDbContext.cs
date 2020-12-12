using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Domain;

namespace TrickingLibrary.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
