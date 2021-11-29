using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Polar.Models
{
    public class PolarContext : IdentityDbContext<User>
    {
        public DbSet<Story> Stories { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Location> Locations { get; set; }

        public PolarContext(DbContextOptions<PolarContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Default value for Stories.Image
            modelBuilder.Entity<Story>()
                .Property(s => s.Image)
                .HasDefaultValue(null);
             // Default value for Users.Score
            modelBuilder.Entity<User>()
                .Property(u => u.Score)
                .HasDefaultValue(0);

            // Configuring Marker-Story 1:1 relationship
            modelBuilder.Entity<Marker>()
                .HasOne(m => m.Story)
                .WithOne(s => s.Marker)
                .HasForeignKey<Story>(s => s.MarkerId);
            // Configuring Marker-Location M:1 relationship
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Markers)
                .WithOne(m => m.Location)
                .HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
            // Configuring User-Marker M:M relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Markers)
                .WithMany(m => m.Users);
        }
    }
}
