using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesCounter.Web.DAL.EntityConfigurations
{
    public class ExpensesListEntityConfiguration : BaseEntityConfiguration<ExpensesList>
    {
        public override void Configure(EntityTypeBuilder<ExpensesList> builder)
        {
            base.Configure(builder);

            builder.HasMany(list => list.Expenses)
                .WithOne(expense => expense.ExpenseList)
                .HasForeignKey(expense => expense.ExpenseListId);

            builder.HasMany(list => list.Users)
                .WithOne(relation => relation.ExpensesList)
                .HasForeignKey(relation => relation.ExpenseListId);
        }
    }
}