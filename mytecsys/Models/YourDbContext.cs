using System.Data.Entity;

namespace mytecsys.Models
{
    public class YourDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfoes { get; set; }

        public YourDbContext() : base("name=YourConnectionString") { }
    }
}
