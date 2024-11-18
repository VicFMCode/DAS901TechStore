using Microsoft.EntityFrameworkCore;
using TechStoreInventory.Models;

namespace TechStoreInventory.Data
{
    public class TechStoreContext : DbContext
    {
        public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

        public DbSet<Celular> Celulares { get; set; }
    }
}
