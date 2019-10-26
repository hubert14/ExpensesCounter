using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesCounter.Web.DAL.EntityConfigurations
{
    public class ExpensesListUserEntityConfiguration : IEntityTypeConfiguration<ExpensesListUser>
    {
        public void Configure(EntityTypeBuilder<ExpensesListUser> builder)
        {
            builder.HasKey(relation => new {relation.UserId, relation.ExpenseListId});
        }
    }
}