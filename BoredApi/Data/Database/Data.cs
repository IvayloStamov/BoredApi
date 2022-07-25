using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Data.Database
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


            //DataSeeding

           //modelBuilder.Entity<Activity>()
           //    .HasData(
           //    new Activity { ActivityId = 1, ActivityName = "education" },
           //    new Activity { ActivityId = 2, ActivityName = "recreational" },
           //    new Activity { ActivityId = 3, ActivityName = "social" },
           //    new Activity { ActivityId = 4, ActivityName = "diy" },
           //    new Activity { ActivityId = 5, ActivityName = "charity" },
           //    new Activity { ActivityId = 6, ActivityName = "cooking" },
           //    new Activity { ActivityId = 7, ActivityName = "relaxation" },
           //    new Activity { ActivityId = 8, ActivityName = "music" },
           //    new Activity { ActivityId = 9, ActivityName = "busywork" }
           //    );

        }
    }
}