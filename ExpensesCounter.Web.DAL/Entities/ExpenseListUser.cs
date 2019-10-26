using System;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class ExpenseListUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ExpenseListId { get; set; }
        public ExpensesList ExpensesList { get; set; }

        public bool IsActiveAssign { get; set; }

        public DateTimeOffset AssignedDateUtc { get; set; }
    }
}