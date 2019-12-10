namespace ExpensesCounter.Common.Models.Expenses
{
    public class UpdateExpenseModel
    {
        public string Comment { get; }

        public decimal Count { get; }

        public UpdateExpenseModel(decimal count, string comment = null)
        {
            Count = count;
            Comment = comment;
        }
    }
}