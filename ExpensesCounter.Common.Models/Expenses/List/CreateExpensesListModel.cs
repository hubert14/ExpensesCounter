namespace ExpensesCounter.Common.Models.Expenses.List
{
    public class CreateExpensesListModel
    {
        public string Title { get; set; }
        public string Comment { get; set; }

        public CreateExpensesListModel(string title, string comment = null)
        {
            Title = title;
            Comment = comment;
        }

        public CreateExpensesListModel()
        {
        }
    }
}