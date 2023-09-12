using ImplementSP.Models;
using Microsoft.EntityFrameworkCore;

namespace ImplementSP.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=PCT198;DataBase=LearnSP;User ID=sa;Password=Tatva@123;Encrypt=False;");

        public virtual DbSet<Product> Products { get; set; }
    }
}
