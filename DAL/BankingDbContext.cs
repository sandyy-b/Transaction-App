using Microsoft.EntityFrameworkCore;
using TransactionApp.Models;

namespace TransactionApp.DAL
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {

        }

        // DBSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
