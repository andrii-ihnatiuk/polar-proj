using Microsoft.EntityFrameworkCore;
 
namespace PolarApi.Models
{
    public class PolarContext : DbContext
    {
        // public DbSet<Worker> Workers { get; set; }
        // public DbSet<Plan> Plans { get; set; }
        // public DbSet<Contract> Contracts { get; set; }
        
        public PolarContext(DbContextOptions<PolarContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
