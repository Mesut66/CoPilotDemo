using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoPilotDemo_VS.Models;

namespace CoPilotDemo_VS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoPilotDemo_VS.Models.Product> Product { get; set; } = default!;
    }
}
