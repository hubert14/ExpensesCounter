namespace ExpensesCounter.Common.Models.Expenses.List
{
    public class UpdateExpensesListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Comment { get; set; }

        public UpdateExpensesListModel(int listId, string title, string comment = null)
        {
            Id = listId;
            Title = title;
            Comment = comment;
        }
    }
}