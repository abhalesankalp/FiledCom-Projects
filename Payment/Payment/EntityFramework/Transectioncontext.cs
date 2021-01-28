using Microsoft.EntityFrameworkCore;
using Payment.Models;

namespace Payment.EntityFramework
{
    public class Transectioncontext : DbContext
    {
        public DbSet<TransectionDetails> TransectionDetails { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=az1db1dev10.northcentralus.cloudapp.azure.com,1433;Initial Catalog=Booker_DEV10;Integrated Security=True");
        }
    }
}
