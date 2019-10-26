using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpensesCounter.Web.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpensesList> ExpensesLists { get; set; }

        public DbSet<ExpensesListUser> UserLists { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}