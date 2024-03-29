using System;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class ExpensesListUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ExpenseListId { get; set; }
        public ExpensesList ExpensesList { get; set; }

        public bool IsActiveAssign { get; set; }

        public DateTimeOffset AssignedDateUtc { get; set; }

        public ExpensesListUser(int userId, int listId)
        {
            UserId = userId;
            ExpenseListId = listId;

            IsActiveAssign = true;
            AssignedDateUtc = DateTime.UtcNow;
        }

        public ExpensesListUser()
        {
        }
    }
}