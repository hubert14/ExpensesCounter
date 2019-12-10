using System.Collections.Generic;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class ExpensesList : BaseEntity
    {
        public string Title { get; set; }

        public string Comment { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<ExpensesListUser> Users { get; set; }
    }
}