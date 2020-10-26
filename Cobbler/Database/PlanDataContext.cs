using Cobbler.DAO;
using Microsoft.EntityFrameworkCore;

namespace Cobbler.Database
{
    public class PlanDataContext : DbContext
    {
        public PlanDataContext(DbContextOptions<PlanDataContext> options) : base(options) { }
        
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
    }
}