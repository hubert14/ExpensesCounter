using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpensesCounter.Web.DAL
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Expense>          Expenses      { get; set; }
        public DbSet<ExpensesList>     ExpensesLists { get; set; }
        public DbSet<ExpensesListUser> UserLists     { get; set; }
        public DbSet<User>             Users         { get; set; }
        public DbSet<RefreshToken>     RefreshTokens { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
                optionsBuilder.UseNpgsql(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}