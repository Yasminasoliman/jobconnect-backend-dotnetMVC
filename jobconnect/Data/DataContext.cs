using jobconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace jobconnect.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User { get; set; }

        public DbSet<Job> Job { get; set; }

        public DbSet<Proposal> Proposal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proposal>().HasKey(sc => new { sc.UserId, sc.JobId });
            modelBuilder.Entity<Proposal>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Proposal)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proposal>()
                .HasOne(sc => sc.Job)
                .WithMany(c => c.Proposal)
                .HasForeignKey(sc => sc.JobId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
