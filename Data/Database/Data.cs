using BoredApi.Data.DataModels;
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

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupActivity> GroupActivities { get; set; }
        public DbSet<JoinActivityRequest> JoinActivityRequests { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(ug => new
            {
                ug.GroupId,
                ug.UserId
            });

            modelBuilder.Entity<GroupActivity>().HasKey(ga => new
            {
                ga.ActivityId,
                ga.GroupId
            });

            modelBuilder.Entity<JoinActivityRequest>().HasKey(jr => new
            {
                jr.ActivityId,
                jr.GroupId,
                jr.UserId
            });
            
            


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