using LostAndFound.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClaimHistory> ClaimHistories { get; set; }

    }
}
