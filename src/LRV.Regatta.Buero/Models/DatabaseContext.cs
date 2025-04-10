using Microsoft.EntityFrameworkCore;

namespace LRV.Regatta.Buero.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<FinishObject> FinishObjects { get; set; }
        public DbSet<RegistrationObject> RegistrationObjects { get; set; }
    }
}