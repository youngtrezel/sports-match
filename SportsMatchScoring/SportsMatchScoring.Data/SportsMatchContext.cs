using Microsoft.EntityFrameworkCore;
using SportsMatchScoring.Shared.Models;

namespace SportsMatchScoring.Data
{
    public class SportsMatchContext : DbContext
    {
        public SportsMatchContext(DbContextOptions<SportsMatchContext> options) : base(options){ }

        public DbSet<MatchRecord> MatchRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchRecord>()
                .HasKey(e => e.Id);
        }
    }
}
