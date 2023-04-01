using Chat.WebAPI.Entities;
using ChatAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // base.Database.EnsureCreated();
            base.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Первичный ключ
            modelBuilder.Entity<User>()
                .HasKey(u => u.Username);

            // Связь один ко многим и каскадное удаление
            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.Username)
                .OnDelete(DeleteBehavior.Cascade);

            // Составной первичный ключ
            modelBuilder.Entity<UserContact>()
                .HasKey(c => new { c.Username, c.ContactUsername });
        }
    }
}
