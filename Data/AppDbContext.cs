using Microsoft.EntityFrameworkCore;

namespace WebApplication16.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Document> Documents { get; set; }
        public DbSet<Models.Admin> Admins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Models.User>()
                .HasMany(u => u.Documents)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Models.Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Models.Admin>(a => a.UserId);
        }
    }
}
