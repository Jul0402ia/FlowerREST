using Microsoft.EntityFrameworkCore;

namespace FlowerREST
{
    public class FlowerDbContext : DbContext
    {
        public FlowerDbContext(DbContextOptions<FlowerDbContext> options) : base(options)
        {
        }
        public DbSet<Flower> Flowers { get; set; } = null!;
    }
}
