using BoredApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Data
{
    public class BoredApiContext : DbContext
    {

        public BoredApiContext(DbContextOptions<BoredApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JoinActivityRequest>()
                .HasKey(ja => new
                {
                    ja.ActivityId,
                    ja.GroupId,
                    ja.UserId
                });

            modelBuilder.Entity<GroupActivity>()
                .HasKey(ga => new
                {
                    ga.ActivityId,
                    ga.GroupId
                });
                

            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new
                {
                    ug.UserId,
                    ug.GroupId
                });

          
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupActivity> GroupActivities { get; set; }
        public DbSet<JoinActivityRequest> JoinActivityRequests { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
    }
}