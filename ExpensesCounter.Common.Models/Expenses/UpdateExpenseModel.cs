namespace ExpensesCounter.Common.Models.Expenses
{
    public class UpdateExpenseModel
    {
        public string Comment { get; set; }

        public decimal Count { get; set; }

        public UpdateExpenseModel(decimal count, string comment = null)
        {
            Count = count;
            Comment = comment;
        }

        public UpdateExpenseModel()
        {
        }
    }
}