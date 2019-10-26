using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesCounter.Web.DAL.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasMany(user => user.RefreshTokens)
                .WithOne(token => token.User)
                .HasForeignKey(token => token.UserId);

            builder.HasMany(user => user.ExpensesLists)
                .WithOne(relation => relation.User)
                .HasForeignKey(relation => relation.UserId);
        }
    }
}