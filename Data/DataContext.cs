using BoredApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Data
{
    public class BoredApiContext : DbContext
    {

        public BoredApiContext(DbContextOptions<BoredApiContext> options) : base(options)
        {

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