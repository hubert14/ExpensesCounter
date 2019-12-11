namespace ExpensesCounter.Common.Models.Expenses
{
    public class AddExpenseModel
    {
        public int ListId { get; set; }
        public string Comment { get; set; }

        public int Count { get; set; }

        public AddExpenseModel(int listId, int count, string comment = null)
        {
            ListId = listId;
            Count = count;
            Comment = comment;
        }

        public AddExpenseModel()
        {
        }
    }
}