namespace ExpensesCounter.Common.Models.Expenses.List
{
    public class UpdateExpensesListModel
    {
        public string Title { get; set; }
        public string Comment { get; set; }

        public UpdateExpensesListModel(string title, string comment = null)
        {
            Title = title;
            Comment = comment;
        }

        public UpdateExpensesListModel()
        {
        }
    }
}