using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserActivity>().HasKey(ua => new
            {
                ua.ActivityId,
                ua.UserId
            });

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(ua => ua.UserActivities)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.Activity)
                .WithMany(ua => ua.UserActivities)
                .HasForeignKey(ua => ua.ActivityId);
        }
    }
}
