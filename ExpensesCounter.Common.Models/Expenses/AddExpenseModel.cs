namespace ExpensesCounter.Common.Models.Expenses
{
    public class AddExpenseModel
    {
        public int ListId { get; }
        public string Comment { get; }

        public decimal Count { get; }

        public AddExpenseModel(int listId, decimal count, string comment = null)
        {
            ListId = listId;
            Count = count;
            Comment = comment;
        }
    }
}