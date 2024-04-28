using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }

        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }

       // protected override void OnConfiguring(DbContextOptionsBuilder options)
       // => options.UseSqlite("Data Source=blogging.db");
    }
}
