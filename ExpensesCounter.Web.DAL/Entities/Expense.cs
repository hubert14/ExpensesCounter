namespace ExpensesCounter.Web.DAL.Entities
{
    public class Expense : BaseEntity
    {
        public int          ExpenseListId { get; set; }
        public ExpensesList ExpenseList   { get; set; }

        public string Comment { get; set; }

        public double Count { get; set; }
    }
}