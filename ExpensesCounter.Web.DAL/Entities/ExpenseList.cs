using System.Collections.Generic;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class ExpensesList : BaseEntity
    {
        public string Title { get; set; }

        public string Comment { get; set; }

        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<ExpenseListUser> Users { get; set; }
    }
}