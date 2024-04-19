using LB12BackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace LB12BackEnd.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated(); // создаем базу данных при первом обращении
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Алексей", Email = "example@gmail.com", Age = 26 }
            );
        }
    }
}
