using Metrices_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Metrices_API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employes> Employes {get;set;}
    }
}
