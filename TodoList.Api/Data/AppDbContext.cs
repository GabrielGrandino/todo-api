using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models;

namespace TodoList.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            var admin = new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "admin123"
            };

            builder.Entity<User>().HasData(admin);
        }
    }
}
