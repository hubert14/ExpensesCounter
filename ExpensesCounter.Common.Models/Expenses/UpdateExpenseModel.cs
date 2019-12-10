namespace ExpensesCounter.Common.Models.Expenses
{
    public class UpdateExpenseModel
    {
        public int Id { get; }

        public string Comment { get; }

        public decimal Count { get; }

        public UpdateExpenseModel(int id, decimal count, string comment = null)
        {
            Id = id;
            Count = count;
            Comment = comment;
        }
    }
}